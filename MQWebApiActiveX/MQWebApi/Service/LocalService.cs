using System;
using System.Collections.Generic;
using System.Text;

namespace MoleQ.Wrapper.WebApi.Service
{
    interface LocalService
    {
        //插入记录
        bool Save(int id, string txt);
        //修改记录
        bool Update(int id, string txt);
        //覆盖记录
        bool SaveOrUpdate(int id, string txt);
        bool SaveOrUpdate2(int id, string txt);
        //移除所有记录
        void RemoveAll();

        bool RemoveOrInsert(int id, string txt);
        bool Remove(int id);
        bool Insert(int id, string txt);

    }
}
