using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoyalLondon.ViewModel;

namespace RoyalLondon.Interface
{
    public interface IDataRepository
    {
        MaturityDataViewModel Get(string policyNumber);

        void Add(MaturityDataViewModel model);

        void Delete(string policyNumber);

        void Update(MaturityDataViewModel model);

        IQueryable<MaturityDataViewModel> GetAll();
    }
}
