/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2017-09-03 11:08:16
 * Update:2017-09-03 11:08:16
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * GitHub: https://github.com/dong666
 * Personal web site: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:
 * Thx , Best Regards ~
 *********************************************************/

package QX_JFrame.Bantina.Bankinate;

import QX_JFrame.Bantina.DBC3P0_Util_DG;
import org.springframework.core.annotation.AnnotationConfigurationException;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;
import java.util.function.Predicate;

public abstract class Bankinate extends DBC3P0_Util_DG {
    /**
     * DataBaseType choice
     */
    private DataBaseType DbType = DataBaseType.Mysql;

    public DataBaseType getDbType() {
        return this.DbType;
    }

    private String SqlStatement;

    public String getSqlStatement() {
        return this.SqlStatement;
    }

    protected Bankinate() {
        super();
    }

    protected Bankinate(DataBaseType dataBaseType) {
        super();
        this.DbType = dataBaseType;
    }

    /**
     * Add
     *
     * @param t   object
     * @param <T> object Type
     * @return
     * @throws Exception
     */
    public <T> boolean Add(T t) throws Exception {
        String tableName = this.GetTableName(t);
        StringBuilder stringBuffer_start = new StringBuilder();
        StringBuilder stringBuffer_end = new StringBuilder();
        stringBuffer_start.append("INSERT INTO ");
        stringBuffer_start.append(tableName);
        stringBuffer_start.append(" (");
        stringBuffer_end.append(" VALUES (");
        Field[] fields = t.getClass().getDeclaredFields();
        List<Object> lists = new ArrayList<>();

        for (Field field : fields) {
            field.setAccessible(true);  //set the field can be visit
            //check AutoIncrease Field
            if (field.isAnnotationPresent(AutoIncrease.class))
                break;
            //check Key and check Column
            if (field.isAnnotationPresent(Key.class) || field.isAnnotationPresent(Column.class)) {
                stringBuffer_start.append(field.getName());
                stringBuffer_end.append("?");
                stringBuffer_start.append(",");
                stringBuffer_end.append(",");
                if (field.getType().getName().equals(UUID.class.getTypeName())) {
                    lists.add(field.get(t).toString().getBytes());
                } else {
                    lists.add(field.get(t));
                }
            }
        }
        stringBuffer_start.append(")");
        stringBuffer_end.append(")");

        //combine sql statement
        String sql = stringBuffer_start.append(stringBuffer_end.toString()).toString().replace(",)", ")");
        return ExecuteSql(sql, lists.toArray());
    }

    public <T> T QueryEntity(Class<T> clazz, Predicate<T> where) {
        String string=where.toString();
        where.negate();
       // String tableName = this.GetTableName(clazz);
        return null;
    }

    public <T> List<T> QueryEntities(Class<T> clazz) throws Exception {
        String tableName = this.GetTableName(clazz);
        //Generate SqlStatement
        String sql = "SELECT * FROM " + tableName;
        this.SqlStatement = sql;
        return QueryList(clazz,sql);
    }

    public <T> List<T> QueryEntities(Class<T> clazz, Predicate<T> where) {

        String string=where.toString();
        where.negate();

        return null;
    }


    /*
    Internal methods --
     */
    private <T> String GetTableName(T t) {
        return GetTableName(t.getClass());
    }

    private <T> String GetTableName(Class<T> clazz) {
        if (clazz.isAnnotationPresent(Table.class)) {
            Table tableAnnotation = clazz.getAnnotation(Table.class);
            String tableName = tableAnnotation.TableName();
            if (tableName.isEmpty() || tableName.equals(""))
                return clazz.getName();
            return tableName;
        }
        throw new AnnotationConfigurationException("Table Annotation not exist -- QX_JFrame.Bantina.Bankinate");
    }
    /*
    -- End Internal Method
     */


}
