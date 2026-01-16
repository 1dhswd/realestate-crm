namespace RealEstateCRM.Application.DTOs.Dashboard
{
    public class DashboardStatsDto
    {
        public int TotalProperties { get; set; }
        public int ActiveProperties { get; set; }
        public int TotalCustomers { get; set; }
        public int TotalLeads { get; set; }
        public int ActiveLeads { get; set; }
        public int TodayAppointments { get; set; }
        public int PendingOffers { get; set; }
        public decimal TotalPropertyValue { get; set; }
        public int NewCustomersThisMonth { get; set; }
        public int ThisMonthAppointments { get; set; }
    }
}