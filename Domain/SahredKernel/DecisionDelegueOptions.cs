namespace ClientMonitoring.Web.Domain.SahredKernel
{
    public class DecisionDelegueOptions
    {
        public Dictionary<string, DecisionDelegue> Decisions { get; set; } = new();
    }

    public class DecisionDelegue
    {


        public string NumDecisionDelegue { get; set; }
        public string DateDecisionDelegue_Ar { get; set; }
        public string DateDecisionDelegue { get; set; }
        public string FullNameDelegue { get; set; }

        public string NumDecisonCommission { get; set; }
        public string DateDecisonCommission_Ar { get; set; }
        public string DateDecisonCommission { get; set; }

        public string CodeWilaya { get; set; }
    }
}
