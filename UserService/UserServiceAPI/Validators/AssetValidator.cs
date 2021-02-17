using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserServiceAPI.DTO;

namespace UserServiceAPI.Validators
{
    /// <summary>
    /// The asset model/DTO validation rule class
    /// </summary>
    public class AssetValidator : AbstractValidator<AssetDTO>
    {
        /// <summary>
        /// The constructor of validator - define the rules here.
        /// </summary>
        public AssetValidator()
        {
            // Validate Scalar Types
            RuleFor(c => c.AssetName).NotEmpty().MaximumLength(255).WithMessage("Please enter the asset name.");
        }
    }
}
