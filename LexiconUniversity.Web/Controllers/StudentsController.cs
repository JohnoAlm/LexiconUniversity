using AutoMapper;
using Bogus;
using LexiconUniversity.Core;
using LexiconUniversity.Data;
using LexiconUniversity.Web.Filters;
using LexiconUniversity.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LexiconUniversity.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly LexiconUniversityContext _context;
        private readonly IMapper mapper;
        private readonly Faker faker;

        public StudentsController(LexiconUniversityContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
            faker = new Faker("sv");
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            //var viewModel = await _context.Student.Select(s => new StudentIndexViewModel
            //{
            //    Id = s.Id,
            //    Avatar = s.Avatar,
            //    FirstName = s.FirstName,
            //    LastName = s.LastName,
            //    AddressStreet = s.Address.Street
            //}).ToListAsync();

            var viewModel = await mapper.ProjectTo<StudentIndexViewModel>(_context.Student)
                .OrderByDescending(s => s.Id)
                .ToListAsync();

            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            //var student = await _context.Student
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var student = await mapper.ProjectTo<StudentDetailsViewModel>(_context.Student)
                                        .FirstOrDefaultAsync(s => s.Id == id);


            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelValidationFilter]
        public async Task<IActionResult> Create(StudentCreateViewModel viewModel)
        {
            //if (ModelState.IsValid)
            //{

            //var student = new Student(faker.Internet.Avatar(), viewModel.FirstName, viewModel.LastName, viewModel.Email)
            //{
            //    Address = new Address
            //    {
            //        City = viewModel.AddressCity,
            //        Street = viewModel.AddressStreet,
            //        ZipCode = viewModel.AddressZipCode
            //    }
            //};

            var student = mapper.Map<Student>(viewModel);
            student.Avatar = faker.Internet.Avatar();

            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //}
            //return View(viewModel);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            //var student = await _context.Student.FindAsync(id);
            var student = await mapper.ProjectTo<StudentEditViewModel>(_context.Student)
                                       .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var student = await _context.Student.Include(s => s.Address)
                        .FirstOrDefaultAsync(s => s.Id == id);

                    mapper.Map(viewModel, student);

                    //_context.Entry(student).Property("Edited").CurrentValue = DateTime.Now;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(viewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'LexiconUniversityContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return (_context.Student?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> CheckIfEmailIsUnique(string email)
        {
            if (await _context.Student.AnyAsync(s => s.Email == email))
            {
                return Json("There is already a student with that email!");
            }
            return Json(true);
        }
    }
}
