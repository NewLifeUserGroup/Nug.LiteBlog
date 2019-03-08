using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using NewLife;
using NewLife.Data;
using NewLife.Log;
using NewLife.Model;
using NewLife.Reflection;
using NewLife.Threading;
using NewLife.Web;
using XCode;
using XCode.Cache;
using XCode.Configuration;
using XCode.DataAccessLayer;
using XCode.Membership;

namespace Nug.Models
{
    /// <summary>分类</summary>
    [Serializable]
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Catalogs : Catalogs<Catalogs> { }

    /// <summary>分类</summary>
    public partial class Catalogs<TEntity> : Entity<TEntity> where TEntity : Catalogs<TEntity>, new()
    {
        #region 对象操作
        static Catalogs()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            var entity = new TEntity();


            // 过滤器 UserModule、TimeModule、IPModule

            // 单对象缓存
            var sc = Meta.SingleCache;
            sc.FindSlaveKeyMethod = k => Find(__.Title, k);
            sc.GetSlaveKeyMethod = e => e.Title;
        }

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew">是否插入</param>
        public override void Valid(Boolean isNew)
        {
            // 如果没有脏数据，则不需要进行任何处理
            if (!HasDirty) return;

            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            if (Title.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Title), "名称不能为空！");
            if (Url.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Url), "链接不能为空！");

            // 在新插入数据或者修改了指定字段时进行修正

            // 检查唯一索引
            // CheckExist(isNew, __.Title);
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Session.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化TEntity[分类]数据……");

            // 顺序依次是 Title Url 
            object[][] ss = new object[][]
            {
                new object[] { "首页", "home"},
                new object[] { "日常", "daily"},
                new object[] { "关于", "about"}
            };

            foreach (object[] objs in ss)
            {
                var entity = new TEntity();
                entity.Title = objs[0].ToString();
                entity.Url = objs[1].ToString();
                entity.Insert();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化TEntity[分类]数据！");
        }

        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnDelete()
        //{
        //    return base.OnDelete();
        //}
        #endregion

        #region 扩展属性
        #endregion

        #region 扩展查询
        /// <summary>根据编号查找</summary>
        /// <param name="id">编号</param>
        /// <returns>实体对象</returns>
        public static TEntity FindByID(Int32 id)
        {
            if (id <= 0) return null;

            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.ID == id);

            // 单对象缓存
            //return Meta.SingleCache[id];

            return Find(_.ID == id);
        }

        /// <summary>根据名称查找</summary>
        /// <param name="title">名称</param>
        /// <returns>实体对象</returns>
        public static TEntity FindByTitle(String title)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Title == title);

            // 单对象缓存
            //return Meta.SingleCache.GetItemWithSlaveKey(title) as TEntity;

            return Find(_.Title == title);
        }

        /// <summary>根据链接查找</summary>
        /// <param name="title">链接</param>
        /// <returns>实体对象</returns>
        public static TEntity FindByUrl(String url)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Url == url);

            // 单对象缓存
            //return Meta.SingleCache.GetItemWithSlaveKey(title) as TEntity;

            return Find(_.Url == url);
        }
        #endregion

        #region 高级查询
        
        #endregion

        #region 业务操作
        #endregion
    }
}