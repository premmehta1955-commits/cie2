
using System.Net.Http.Json;
using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HospitalAppointmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemoryCache _cache;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(
            IMemoryCache cache,
            IHttpClientFactory httpClientFactory)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
        }

        private List<Doctor> GetDoctors()
        {
            return new List<Doctor>
            {
                new Doctor
                {
                    DoctorId = 1,
                    DoctorName = "Dr. Amit Patel",
                    Specialization = "Cardiology",
                    Experience = 10,
                    ConsultationFee = 500
                },
                new Doctor
                {
                    DoctorId = 2,
                    DoctorName = "Dr. Neha Shah",
                    Specialization = "Dermatology",
                    Experience = 7,
                    ConsultationFee = 400
                },
                new Doctor
                {
                    DoctorId = 3,
                    DoctorName = "Dr. Raj Mehta",
                    Specialization = "Orthopedics",
                    Experience = 12,
                    ConsultationFee = 600
                }
            };
        }

        public IActionResult Index(string? specialization)
        {
            // Cookie: store patient name
            if (Request.Cookies["PatientName"] == null)
            {
                Response.Cookies.Append(
                    "PatientName",
                    "Guest Patient",
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(7),
                        HttpOnly = true,
                        IsEssential = true
                    });
            }

            // Session: store selected specialization
            if (!string.IsNullOrWhiteSpace(specialization))
            {
                HttpContext.Session.SetString(
                    "Specialization", specialization);
            }

            ViewBag.PatientName =
                Request.Cookies["PatientName"] ?? "Guest Patient";

            ViewBag.Specialization =
                HttpContext.Session.GetString("Specialization");

            // IMemoryCache: cache doctor list for 3 minutes
            var doctors = _cache.GetOrCreate("DoctorList", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(3);

                return GetDoctors();
            })!;

            return View(doctors);
        }

        // Query string: /Home/Details?doctorId=1
        [ResponseCache(
            Duration = 60,
            Location = ResponseCacheLocation.Any,
            NoStore = false)]
        public IActionResult Details(int doctorId)
        {
            var doctors = _cache.GetOrCreate("DoctorList", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(3);

                return GetDoctors();
            })!;

            var doctor = doctors.FirstOrDefault(
                d => d.DoctorId == doctorId);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpGet]
        public IActionResult Appointment(int doctorId)
        {
            var doctors = _cache.GetOrCreate("DoctorList", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(3);

                return GetDoctors();
            })!;

            var doctor = doctors.FirstOrDefault(
                d => d.DoctorId == doctorId);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Appointment(
            int doctorId, string patientName)
        {
            var doctors = _cache.GetOrCreate("DoctorList", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromMinutes(3);

                return GetDoctors();
            })!;

            var doctor = doctors.FirstOrDefault(
                d => d.DoctorId == doctorId);

            if (doctor == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(patientName))
            {
                ModelState.AddModelError(
                    "patientName", "Patient name is required.");

                return View(doctor);
            }

            // Update the patient name cookie
            Response.Cookies.Append(
                "PatientName",
                patientName.Trim(),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    HttpOnly = true,
                    IsEssential = true
                });

            ViewBag.PatientName = patientName.Trim();

            return View("Confirmation", doctor);
        }

        // Get health and wellness articles
        public IActionResult Posts()
        {
            // Sample health articles in professional English
            var posts = new List<Post>
            {
                new Post
                {
                    Id = 1,
                    UserId = 101,
                    Title = "Understanding Hypertension: Prevention and Management",
                    Body = "Hypertension, commonly known as high blood pressure, is a serious medical condition that affects millions of people worldwide. It occurs when the force of blood against arterial walls is consistently too high, typically defined as 130/80 mmHg or higher. High blood pressure is often called a 'silent killer' because many people have it without knowing, as it usually has no symptoms. Over time, uncontrolled hypertension can lead to serious health complications including heart disease, stroke, kidney disease, and vision problems. Risk factors include age, family history, obesity, excessive salt intake, stress, and sedentary lifestyle. Prevention strategies include maintaining a healthy weight, regular exercise, reducing sodium intake, limiting alcohol, managing stress, and eating a heart-healthy diet rich in fruits and vegetables. Treatment may involve lifestyle modifications and medications prescribed by your doctor. Regular blood pressure monitoring is essential for early detection and management."
                },
                new Post
                {
                    Id = 2,
                    UserId = 102,
                    Title = "Cardiovascular Health: The Importance of Heart Care",
                    Body = "The heart is the most vital organ in the human body, pumping blood and oxygen to every cell. Cardiovascular diseases remain the leading cause of death globally, making heart health awareness crucial. Key aspects of maintaining a healthy heart include regular physical activity, maintaining healthy cholesterol levels, controlling diabetes, avoiding smoking, and managing stress. Exercise strengthens the heart muscle and improves circulation. A healthy diet rich in omega-3 fatty acids, fiber, and antioxidants supports cardiovascular function. Regular check-ups with a cardiologist can help identify risk factors early. Warning signs of heart problems include chest pain, shortness of breath, irregular heartbeat, and fatigue. Understanding your family history of heart disease is important, as genetics play a significant role. Modern treatments like stents, medication therapy, and lifestyle changes have significantly improved outcomes for heart patients. Consulting with a qualified cardiologist is recommended for personalized prevention and treatment strategies."
                },
                new Post
                {
                    Id = 3,
                    UserId = 103,
                    Title = "Diabetes Management: Living Well with Type 2 Diabetes",
                    Body = "Type 2 diabetes is a chronic condition characterized by high blood glucose levels resulting from insulin resistance or insufficient insulin production. It affects over 400 million people worldwide and is rapidly increasing due to sedentary lifestyles and obesity. Symptoms may include increased thirst, frequent urination, fatigue, and slow-healing wounds. Early detection through regular blood glucose screening is vital for preventing serious complications. Management involves a combination of lifestyle modifications and medications. A balanced diet with controlled carbohydrate intake, regular physical activity, weight management, and stress reduction are fundamental. Medication options include metformin, sulfonylureas, and insulin therapy, which should be prescribed based on individual needs assessed by healthcare professionals. Regular monitoring of blood glucose levels through home testing or HbA1c tests every three months helps track disease control. Complications of unmanaged diabetes can include heart disease, kidney disease, neuropathy, and vision problems. With proper management and support, people with diabetes can lead healthy, fulfilling lives while significantly reducing the risk of serious complications."
                },
                new Post
                {
                    Id = 4,
                    UserId = 104,
                    Title = "Mental Health Matters: Stress Management and Wellness",
                    Body = "Mental health is as important as physical health, yet often overlooked in our busy modern lives. Chronic stress, anxiety, and depression affect millions of people and can have serious impacts on overall well-being. Stress is a natural response to challenges, but prolonged stress can lead to various health problems including high blood pressure, weakened immunity, and sleep disorders. Effective stress management techniques include regular exercise, meditation, deep breathing exercises, adequate sleep, and maintaining healthy relationships. Therapy and counseling services from qualified mental health professionals can provide valuable support. Cognitive behavioral therapy, mindfulness practices, and relaxation techniques have proven effective for many conditions. Physical activity is a powerful stress reliever, releasing endorphins that improve mood. Social connections and a strong support network are crucial for mental well-being. It's important to recognize warning signs such as persistent sadness, loss of interest in activities, or thoughts of self-harm, and seek professional help immediately. Prioritizing mental health through preventive measures and early intervention can significantly improve quality of life and prevent serious health conditions."
                },
                new Post
                {
                    Id = 5,
                    UserId = 105,
                    Title = "Orthopedic Health: Prevention and Treatment of Joint Pain",
                    Body = "Orthopedic health involves the care of bones, joints, muscles, and ligaments. Joint pain and mobility issues affect people of all ages, from athletes to elderly individuals. Common conditions include arthritis, back pain, sprains, and fractures. Osteoarthritis, the most common type of arthritis, develops due to wear and tear of joint cartilage over time. Risk factors include age, obesity, previous injuries, and repetitive stress. Prevention strategies include maintaining healthy weight, staying physically active with low-impact exercises, proper ergonomics, and avoiding high-impact activities that stress joints. Early intervention is key when experiencing joint pain or mobility issues. Treatment options range from conservative approaches like physical therapy and anti-inflammatory medications to surgical interventions when necessary. Physiotherapy and targeted exercises can strengthen muscles supporting joints and improve flexibility. Proper footwear, correct posture, and ergonomic workspaces reduce stress on joints. Regenerative medicine approaches like stem cell therapy show promising results for certain conditions. Professional orthopedic consultation is recommended for persistent joint pain or mobility limitations to develop an appropriate treatment plan and prevent long-term complications."
                },
                new Post
                {
                    Id = 6,
                    UserId = 106,
                    Title = "Dermatology Essentials: Caring for Your Skin",
                    Body = "The skin is the largest organ of the human body, protecting us from environmental threats and regulating body temperature. Dermatological health is both a medical concern and an aesthetic one. Common skin conditions include acne, eczema, psoriasis, melanoma, and various infections. Acne affects adolescents and adults, causing inflammation and can impact self-confidence. Eczema and psoriasis are chronic inflammatory conditions requiring ongoing management. Skin cancer, including melanoma, is the most common cancer worldwide, often caused by excessive sun exposure. Prevention is the first line of defense: use broad-spectrum sunscreen with SPF 30 or higher, seek shade during peak hours, wear protective clothing, and perform regular self-examinations. Early detection of skin changes like moles developing irregular borders, changing color, or growing in size requires immediate dermatological evaluation. Treatment options vary from topical medications to systemic therapies depending on the condition and severity. Professional dermatologists can provide accurate diagnosis and personalized treatment plans. Maintaining skin health through proper cleansing, moisturizing, and sun protection helps prevent many common skin problems. Regular dermatological check-ups, especially for those with family history of skin cancer, are important preventive measures."
                }
            };

            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
    }
}
