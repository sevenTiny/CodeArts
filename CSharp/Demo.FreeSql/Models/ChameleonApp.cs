
using FreeSql.DataAnnotations;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices;

namespace Demo.FreeSql.Models
{
    [Table(Name = "chameleonapp_2")]
    public class ChameleonApp
    {
        /// <summary>
        /// 数据Id
        /// </summary>
        [Column(IsPrimary = true)]
        public Guid _id { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 应用编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 应用Icon
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public double Number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// text type
        /// </summary>
        [JsonMap]
        public User User { get; set; }
        /// <summary>
        /// json type
        /// </summary>
        public JObject User2 { get; set; }
        public string[] StrArray { get; set; }
        public int[] IntArray { get; set; }
    }

    public enum Type
    {
        Basic = 0,
        Senior = 1
    }

    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

/** 
-- public.chameleonapp_2 definition

-- Drop table

-- DROP TABLE public.chameleonapp_2;

CREATE TABLE public.chameleonapp_2 (
	"_id" uuid NOT NULL,
	"name" text NULL,
	code text NULL,
	createdby text NULL,
	createdtime timestamptz NULL,
	icon text NULL,
	description text NULL,
	isdeleted bool NOT NULL DEFAULT false,
	"number" float8 NULL,
	"type" int4 NULL,
	"user" text NULL,
	user2 jsonb NULL,
	intarray int[] NULL,
	strarray text[] NULL,
	CONSTRAINT test_pkey PRIMARY KEY ("_id")
);
 */