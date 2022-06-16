namespace Usi_Project.Repository.DrugRepository
{
    public interface IDrugRepository
    {
        void LoadData();
        void SaveData();
        void AddDrug(Drug drug);
        void RemoveRejectedDrug(RejectedDrug drug);

    }
}