﻿using Coldairarrow.Entity.PB;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.PB
{
    public class PB_RackBusiness : BaseBusiness<PB_Rack>, IPB_RackBusiness, ITransientDependency
    {
        public PB_RackBusiness(IRepository repository)
            : base(repository)
        {
        }

        #region 外部接口

        public async Task<PageResult<PB_Rack>> GetDataListAsync(PB_RackPageInput input)
        {
            var q = GetIQueryable().Where(w => w.StorId == input.StorId);
            var where = LinqHelper.True<PB_Rack>();
            var search = input.Search;

            if (!search.Name.IsNullOrEmpty())
                where = where.And(w => w.Name.Contains(search.Name));
            if (!search.Code.IsNullOrEmpty())
                where = where.And(w => w.Code.Contains(search.Code));

            return await q.Where(where).GetPageResultAsync(input);
        }

        public async Task<PB_Rack> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(PB_Rack data)
        {
            await InsertAsync(data);
        }

        public async Task UpdateDataAsync(PB_Rack data)
        {
            await UpdateAsync(data);
        }

        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        public async Task<List<PB_Rack>> QueryRackDataAsync()
        {
            var q = GetIQueryable();

            return await q.ToListAsync();
        }

        public async Task<List<PB_Rack>> GetDataListAsync(string storId)
        {
            var q = GetIQueryable();
            q = q.Where(w => w.StorId == storId);

            return await q.ToListAsync();
        }

        #endregion

        #region 私有成员

        #endregion
    }
}