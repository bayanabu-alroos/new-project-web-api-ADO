namespace TechnicalQuestionAPI_ADO.DTO
{
    public class ExpierincesDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime Enddate { get; set; }
        public string CompanyName { get; set; }
        public int USERID { get; set; }
        public string NationalityId { get; set; }
        public bool IsActive { get; set; }
    }
}
