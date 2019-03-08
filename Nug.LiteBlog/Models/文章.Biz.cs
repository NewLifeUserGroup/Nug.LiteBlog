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
    /// <summary>文章</summary>
    [Serializable]
    [ModelCheckMode(ModelCheckModes.CheckTableWhenFirstUse)]
    public class Posts : Posts<Posts> { }

    /// <summary>文章</summary>
    public partial class Posts<TEntity> : Entity<TEntity> where TEntity : Posts<TEntity>, new()
    {
        #region 对象操作
        static Posts()
        {
            // 用于引发基类的静态构造函数，所有层次的泛型实体类都应该有一个
            var entity = new TEntity();

            // 累加字段
            //var df = Meta.Factory.AdditionalFields;
            //df.Add(__.CatalogsID);

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
            if (Title.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Title), "标题不能为空！");
            if (Summary.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Summary), "摘要不能为空！");
            if (Content.IsNullOrEmpty()) throw new ArgumentNullException(nameof(Content), "内容不能为空！");

            // 在新插入数据或者修改了指定字段时进行修正

            // 检查唯一索引
            // CheckExist(isNew, __.Title);
            // CheckExist(isNew, __.Url);
        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            if (Meta.Session.Count > 0) return;

            if (XTrace.Debug) XTrace.WriteLine("开始初始化TEntity[文章]数据……");

            try
            {
                Posts.Meta.Session.BeginTrans();

                for (int i = 0; i < 7; i++)
                {
                    var entity = new TEntity();
                    entity.Url = "sanwei" + i;
                    entity.Title = "三味书屋" + i;
                    entity.Summary = "&nbsp;&nbsp;&nbsp;&nbsp;先生读书入神的时候，于我们是很相宜的。有几个便用纸糊的盔甲套在指甲上做戏。我是画画儿，用一种叫作“荆川纸”的，蒙在小说的绣像上一个个描下来，象习字时候的影写一样。读的书多起来，画的画也多起来;书没有读成，画的成绩却不少了，最成片断的是&lt;荡寇志&gt;和&lt;西游记&gt;的绣像，都有一大本。后来，因为要钱用，卖给一个有钱的同窗了。他的父亲是开锡箔店的;听说现在自己已经做了店主，而且快要升到绅士的地位了。这东西早已没有了罢。";
                    entity.Content = "<p>&nbsp;&nbsp;&nbsp;&nbsp;先生读书入神的时候，于我们是很相宜的。有几个便用纸糊的盔甲套在指甲上做戏。我是画画儿，用一种叫作“荆川纸”的，蒙在小说的绣像上一个个描下来，象习字时候的影写一样。读的书多起来，画的画也多起来;书没有读成，画的成绩却不少了，最成片断的是&lt;荡寇志&gt;和&lt;西游记&gt;的绣像，都有一大本。后来，因为要钱用，卖给一个有钱的同窗了。他的父亲是开锡箔店的;听说现在自己已经做了店主，而且快要升到绅士的地位了。这东西早已没有了罢。</p>";
                    entity.Time = DateTime.Now;
                    entity.CatalogsID = new Random().Next(2, 4);
                    entity.Type = 1;
                    entity.Insert();
                }

                Posts.Meta.Session.Commit();
            }
            catch (Exception ex)
            {
                if (XTrace.Debug) XTrace.WriteLine("初始化TEntity[文章]数据失败！原因：" + ex.Message);

                Posts.Meta.Session.Rollback();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化TEntity[文章]数据！");
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
        /// <summary>分类ID</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        public Catalogs Catalogs { get { return Extends.Get(nameof(Catalogs), k => Catalogs.FindByID(CatalogsID)); } }

        /// <summary>分类ID</summary>
        [XmlIgnore]
        //[ScriptIgnore]
        [DisplayName("分类ID")]
        [Map(__.CatalogsID, typeof(Catalogs), "ID")]
        public String CatalogsTitle { get { return Catalogs?.Title; } }
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

        /// <summary>根据标题查找</summary>
        /// <param name="title">标题</param>
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
        /// <param name="url">链接</param>
        /// <returns>实体对象</returns>
        public static TEntity FindByUrl(String url)
        {
            // 实体缓存
            if (Meta.Session.Count < 1000) return Meta.Cache.Find(e => e.Url == url);

            return Find(_.Url == url);
        }
        #endregion

        #region 高级查询

        public static IList<TEntity> PageView(int catalogsId, PageParameter param)
        {
            var exp = new WhereExpression();

            if (catalogsId != 0) exp += _.CatalogsID == catalogsId;

            return FindAll(exp, "Time desc", "", (param.PageIndex - 1) * param.PageSize, param.PageSize);
        }
        #endregion

        #region 业务操作
        #endregion
    }
}