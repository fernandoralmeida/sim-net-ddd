using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Sim.UI.Web.Pages.Pessoa
{

    using Sim.Domain.SDE.Entity;
    using Sim.Application.SDE.Interface;

    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly IAppServicePessoa _pessoa;
        private readonly IMapper _mapper;

        public DeleteModel(IAppServicePessoa servicePessoa, IMapper mapper)
        {
            _pessoa = servicePessoa;
            _mapper = mapper;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public EditModel.InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t = Task.Run(
                () => _pessoa.GetById((Guid)id));

            await t;

            Input = _mapper.Map<EditModel.InputModel>(t.Result);

            if (Input == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var t = Task.Run(
                () => _pessoa.GetById((Guid)id));

            await t;

            if (t.Result != null)
            {
                _pessoa.Remove(t.Result);
            }

            return RedirectToPage("./Index");
        }
    }
}
