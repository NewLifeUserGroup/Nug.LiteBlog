using System;
using System.Collections.Generic;
using System.ComponentModel;
using XCode;
using XCode.Configuration;
using XCode.DataAccessLayer;

namespace Nug.Models
{
    /// <summary>文章</summary>
    [Serializable]
    [DataObject]
    [Description("文章")]
    [BindIndex("IU_Posts_Title", true, "Title")]
    [BindIndex("IU_Posts_Url", true, "Url")]
    [BindTable("Posts", Description = "文章", ConnName = "Conn", DbType = DatabaseType.SqlServer)]
    public partial class Posts<TEntity> : IPosts
    {
        #region 属性
        private Int32 _ID;
        /// <summary>编号</summary>
        [DisplayName("编号")]
        [Description("编号")]
        [DataObjectField(true, true, false, 0)]
        [BindColumn("ID", "编号", "int")]
        public Int32 ID { get { return _ID; } set { if (OnPropertyChanging(__.ID, value)) { _ID = value; OnPropertyChanged(__.ID); } } }

        private String _Url;
        /// <summary>链接</summary>
        [DisplayName("链接")]
        [Description("链接")]
        [DataObjectField(false, false, true, 50)]
        [BindColumn("Url", "链接", "nvarchar(50)")]
        public String Url { get { return _Url; } set { if (OnPropertyChanging(__.Url, value)) { _Url = value; OnPropertyChanged(__.Url); } } }

        private String _Title;
        /// <summary>标题</summary>
        [DisplayName("标题")]
        [Description("标题")]
        [DataObjectField(false, false, false, 50)]
        [BindColumn("Title", "标题", "nvarchar(50)", Master = true)]
        public String Title { get { return _Title; } set { if (OnPropertyChanging(__.Title, value)) { _Title = value; OnPropertyChanged(__.Title); } } }

        private String _Summary;
        /// <summary>摘要</summary>
        [DisplayName("摘要")]
        [Description("摘要")]
        [DataObjectField(false, false, false, 500)]
        [BindColumn("Summary", "摘要", "nvarchar(500)")]
        public String Summary { get { return _Summary; } set { if (OnPropertyChanging(__.Summary, value)) { _Summary = value; OnPropertyChanged(__.Summary); } } }

        private String _Content;
        /// <summary>内容</summary>
        [DisplayName("内容")]
        [Description("内容")]
        [DataObjectField(false, false, false, -1)]
        [BindColumn("Content", "内容", "ntext")]
        public String Content { get { return _Content; } set { if (OnPropertyChanging(__.Content, value)) { _Content = value; OnPropertyChanged(__.Content); } } }

        private DateTime _Time;
        /// <summary>时间</summary>
        [DisplayName("时间")]
        [Description("时间")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Time", "时间", "datetime")]
        public DateTime Time { get { return _Time; } set { if (OnPropertyChanging(__.Time, value)) { _Time = value; OnPropertyChanged(__.Time); } } }

        private Int32 _CatalogsID;
        /// <summary>分类ID</summary>
        [DisplayName("分类ID")]
        [Description("分类ID")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("CatalogsID", "分类ID", "int")]
        public Int32 CatalogsID { get { return _CatalogsID; } set { if (OnPropertyChanging(__.CatalogsID, value)) { _CatalogsID = value; OnPropertyChanged(__.CatalogsID); } } }

        private Int32 _Type;
        /// <summary>内容类型，markdown/html</summary>
        [DisplayName("内容类型")]
        [Description("内容类型，markdown/html")]
        [DataObjectField(false, false, false, 0)]
        [BindColumn("Type", "内容类型，markdown/html", "int")]
        public Int32 Type { get { return _Type; } set { if (OnPropertyChanging(__.Type, value)) { _Type = value; OnPropertyChanged(__.Type); } } }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        public override Object this[String name]
        {
            get
            {
                switch (name)
                {
                    case __.ID : return _ID;
                    case __.Url : return _Url;
                    case __.Title : return _Title;
                    case __.Summary : return _Summary;
                    case __.Content : return _Content;
                    case __.Time : return _Time;
                    case __.CatalogsID : return _CatalogsID;
                    case __.Type : return _Type;
                    default: return base[name];
                }
            }
            set
            {
                switch (name)
                {
                    case __.ID : _ID = Convert.ToInt32(value); break;
                    case __.Url : _Url = Convert.ToString(value); break;
                    case __.Title : _Title = Convert.ToString(value); break;
                    case __.Summary : _Summary = Convert.ToString(value); break;
                    case __.Content : _Content = Convert.ToString(value); break;
                    case __.Time : _Time = Convert.ToDateTime(value); break;
                    case __.CatalogsID : _CatalogsID = Convert.ToInt32(value); break;
                    case __.Type : _Type = Convert.ToInt32(value); break;
                    default: base[name] = value; break;
                }
            }
        }
        #endregion

        #region 字段名
        /// <summary>取得文章字段信息的快捷方式</summary>
        public partial class _
        {
            /// <summary>编号</summary>
            public static readonly Field ID = FindByName(__.ID);

            /// <summary>链接</summary>
            public static readonly Field Url = FindByName(__.Url);

            /// <summary>标题</summary>
            public static readonly Field Title = FindByName(__.Title);

            /// <summary>摘要</summary>
            public static readonly Field Summary = FindByName(__.Summary);

            /// <summary>内容</summary>
            public static readonly Field Content = FindByName(__.Content);

            /// <summary>时间</summary>
            public static readonly Field Time = FindByName(__.Time);

            /// <summary>分类ID</summary>
            public static readonly Field CatalogsID = FindByName(__.CatalogsID);

            /// <summary>内容类型，markdown/html</summary>
            public static readonly Field Type = FindByName(__.Type);

            static Field FindByName(String name) { return Meta.Table.FindByName(name); }
        }

        /// <summary>取得文章字段名称的快捷方式</summary>
        public partial class __
        {
            /// <summary>编号</summary>
            public const String ID = "ID";

            /// <summary>链接</summary>
            public const String Url = "Url";

            /// <summary>标题</summary>
            public const String Title = "Title";

            /// <summary>摘要</summary>
            public const String Summary = "Summary";

            /// <summary>内容</summary>
            public const String Content = "Content";

            /// <summary>时间</summary>
            public const String Time = "Time";

            /// <summary>分类ID</summary>
            public const String CatalogsID = "CatalogsID";

            /// <summary>内容类型，markdown/html</summary>
            public const String Type = "Type";
        }
        #endregion
    }

    /// <summary>文章接口</summary>
    public partial interface IPosts
    {
        #region 属性
        /// <summary>编号</summary>
        Int32 ID { get; set; }

        /// <summary>链接</summary>
        String Url { get; set; }

        /// <summary>标题</summary>
        String Title { get; set; }

        /// <summary>摘要</summary>
        String Summary { get; set; }

        /// <summary>内容</summary>
        String Content { get; set; }

        /// <summary>时间</summary>
        DateTime Time { get; set; }

        /// <summary>分类ID</summary>
        Int32 CatalogsID { get; set; }

        /// <summary>内容类型，markdown/html</summary>
        Int32 Type { get; set; }
        #endregion

        #region 获取/设置 字段值
        /// <summary>获取/设置 字段值</summary>
        /// <param name="name">字段名</param>
        /// <returns></returns>
        Object this[String name] { get; set; }
        #endregion
    }
}