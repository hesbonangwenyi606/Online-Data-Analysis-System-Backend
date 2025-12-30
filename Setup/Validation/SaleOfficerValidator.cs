using FluentValidation;
using FOS.DataLayer;
using FOS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Setup.Validation
{
    public class SaleOfficerValidator : AbstractValidator<SaleOfficerData>
    {
        public SaleOfficerValidator()
        {
            //RuleFor(RH => RH.Name).NotEmpty().WithMessage("* Required");
            RuleFor(RH => RH.Phone1).Must(BeUniquePhone1).WithMessage("Phone1 Already Exist");
            RuleFor(RH => RH.Phone2).Must(BeUniquePhone2).WithMessage("Phone2 Already Exist");
        }

        private bool BeUniquePhone1(string strPhone1)
        {
            using (FOSDataModel dbContext = new FOSDataModel())
            {
                Boolean boolFlag = true;
                if (!String.IsNullOrEmpty(strPhone1))
                {
                    if (dbContext.SaleOfficers.FirstOrDefault(x => x.Phone1 == strPhone1) == null) return true;
                    boolFlag = false;
                }
                return boolFlag;
            }
        }

        private bool BeUniquePhone2(string strPhone2)
        {
            Boolean boolFlag = true;
            if (!String.IsNullOrEmpty(strPhone2))
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    if (dbContext.SaleOfficers.FirstOrDefault(x => x.Phone2 == strPhone2) == null) return true;
                    boolFlag = false;
                }
            }
            return boolFlag;
        }
    }
}