using System.ComponentModel.DataAnnotations;

namespace BUGGAFIT_BACK.Clases
{
    public class IntMinValueValidator : ValidationAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public IntMinValueValidator()
        {
            this.Min = 0;
            this.Max = int.MaxValue;
        }

        public override bool IsValid(object? value)
        {
            int? intValue = value as int?;
            if (intValue is not null)
                return intValue >= this.Min && intValue <= this.Max;
            return true;
        }
    }
}
