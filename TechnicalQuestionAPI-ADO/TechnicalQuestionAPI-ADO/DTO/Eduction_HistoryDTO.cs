namespace TechnicalQuestionAPI_ADO.DTO
{
    public class Eduction_HistoryDTO
    {
        public string Title { get; set; }
        public string Specification { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public string Description { get; set; }
        public string Orginzation_Name { get; set; }
        public int USERID { get; set; }
        public string NationalityId { get; set; }
        public bool IsActive { get; set; }
    }
}
