using CEDIS.Core.Pgsql.Domain;
using CEDIS.Core.Pgsql.Frameworks.Helpers;
using CEDIS.Core.Pgsql.Persistences;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CEDIS.Core.Pgsql.Services
{
    public class BoxService
    {
        private readonly int ENTITY_ID_INCREMENT = 1;
        private readonly int ZERO_INCREMENT_ON_REQUEST = 1000;
        private readonly ApplicationDbContext _dbContext;

        public BoxService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Box>> GetAllBox()
        {
            return await _dbContext.Boxes.ToListAsync();
        }

        public async Task<Box> GetBoxById(int boxId, int warehouseId)
        {
            return await _dbContext.Boxes.FirstOrDefaultAsync(x=>x.Id==boxId && x.WarehouseId==warehouseId);
        }

        public async Task<Boolean> AddBox(int cant,int warehouseId)
        {
            var currentId = IdentityIncrement<Box>.GetNextId(_dbContext, nameof(Box.WarehouseId), warehouseId.ToString(), nameof(Box.Id), ZERO_INCREMENT_ON_REQUEST);
            var NewBoxes = new List<Box>();
            for(int i = 0; i<cant; i++)
            {
                currentId += ENTITY_ID_INCREMENT;
                NewBoxes.Add(new Box()
                {
                    Id = currentId,
                    WarehouseId = warehouseId
                });
            }
            _dbContext.Boxes.AddRange(NewBoxes);
            return await _dbContext.SaveChangesAsync()>0;
        }
    }
}
