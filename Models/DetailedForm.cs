using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class DetailedForm
    {
        [Required]
        [Display(Name = "Imię")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "PESEL")]
        public string PESEL { get; set; }

        [Required]
        [Display(Name = "Wiek")]
        [Range(0, 1200)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        public bool? HaveAnyOfDiseases { get; set; }


        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        public bool? IfReceiveImmunosuppressiveMed { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy masz gorączkę (temperaturę powyżej +37.3°C) lub czy jej doświadczyłeś w przeciągu ostatnich 14 dni?")]
        public bool? HighTemperature { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy w przeciągu ostatnich 14 dni miałeś kaszel lub objawy duszności?")]
        public bool? CoughNBreathProblems { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy w ciągu ostatnich 14 dni podróżowałeś(aś) za granicę?")]
        public bool? IfPersonTravelled { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy w ciągu ostatnich 14 dni miałeś(aś) kontakt z osobą, która przyjechała z zagranicy, bądź miała ona gorączkę lub objawy duszności?")]
        public bool? ContactWithSuspect { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy przebywałeś ostatnio w miejscach, w których miałeś bliski kontakt z nieznanymi osobami?")]
        public bool? ContactWithUnacquainted { get; set; }

        [Display(Name = "Czy doświadczyłeś(aś) któryś z wymienionych objawów w przeciągu ostatnich 14 dni (jeśli tak to których)?")]
        public string SymptomsWhichPersonExperienced { get; set; }

        public string LungDisease { get; set; }

        public string HeartDisease { get; set; }

        public string ChronicKidneyDisease { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy jesteś w ciązy?")]
        public bool? AreYouPregnant { get; set; }

        [Required(ErrorMessage = "Proszę wybrać jedną z opcji!")]
        [Display(Name = "Czy ostatnio byłeś leczony na jakiegoś rodzaju raka?")]
        public bool? CancerTreated { get; set; }
    }
}