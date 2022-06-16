namespace Usi_Project
{
    public interface IViewerSurgeryEquipments
    {
        void PrintSurgeryEquipments(string equipment, int parameterOfSearch);
    }

    public interface IViewerMedicalEquipments
    {
        void PrintMedicalEquipments(string equipment, int parameterOfSearch);   

    }
}