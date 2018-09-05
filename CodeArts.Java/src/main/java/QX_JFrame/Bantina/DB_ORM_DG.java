package QX_JFrame.Bantina;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import java.lang.reflect.Field;

/**
 * Created by qixiao on 2016/9/2.
 */
public final class DB_ORM_DG extends DBC3P0_Util_DG {
    /**
     * Private constructor, limiting the class cannot be instantiated
     */
    private DB_ORM_DG() {
        throw new Error("The class Cannot be instance !");
    }

    /**
     * use log4j print the error info
     */
    private static final Log log = LogFactory.getLog(DB_ORM_DG.class);

    /**
     * info:only support one Identity you should ask the position of it start at 0 and if not have you should input the num<=1 like -1
     * Like DB_ORM_DG.orm_insertTable("tb_test",test,0) the test is an object
     *
     * @param tableName
     * @param t
     * @param IdentityPosition
     * @param <T>
     * @return
     */
    public static <T> boolean orm_insertTable(String tableName, T t, int IdentityPosition) {
        try {
            StringBuffer stringBuffer1 = new StringBuffer();
            StringBuffer stringBuffer2 = new StringBuffer();
            stringBuffer1.append("insert into ");
            stringBuffer1.append(tableName);
            stringBuffer1.append(" ( ");
            stringBuffer2.append(" VALUES ( ");
            Field[] fs = t.getClass().getDeclaredFields();
            Object[] objects = new Object[IdentityPosition > -1 ? fs.length - 1 : fs.length];
            for (int i = 0, j = 0; i < fs.length; i++, j++) {
                if (i == IdentityPosition - 1) {
                    j--;
                    continue;
                }
                Field f = fs[i];
                f.setAccessible(true);//set the field can be visit
                stringBuffer1.append(f.getName());
                stringBuffer2.append("?");
                objects[j] = f.get(t);
                if (i < fs.length - 1) {
                    stringBuffer1.append(",");
                    stringBuffer2.append(",");
                } else {
                    stringBuffer1.append(" )");
                    stringBuffer2.append(" );");
                }
            }
            String sql = stringBuffer1.toString() + stringBuffer2.toString();
            return ExecuteSql(sql, objects);
        } catch (IllegalAccessException e) {
            log.info(e);
            return false;
        } catch (Exception ee) {
            log.info(ee);
            return false;
        }
    }

    /**
     * Like DB_ORM_DG.orm_updateTable("tb_test",test,0,"testId","and Id") the test is an object
     *
     * @param tableName
     * @param t
     * @param IdentityPosition
     * @param conditionAndFields
     * @param <T>
     * @return
     */
    public static <T> boolean orm_updateTable(String tableName, T t, int IdentityPosition, String... conditionAndFields) {
        try {
            StringBuffer stringBuffer = new StringBuffer();
            stringBuffer.append("update ");
            stringBuffer.append(tableName);
            stringBuffer.append(" set ");
            Field[] fs = t.getClass().getDeclaredFields();
            Object[] objects = new Object[fs.length];
            int objectsPosition = 0;
            for1:
            for (int i = 0, j = 0; i < fs.length; i++, j++) {
                if (i == IdentityPosition - 1) {
                    j--;
                    continue;
                }
                Field f = fs[i];
                for (int k = 0; k < conditionAndFields.length; k++) {
                    String[] conditions = conditionAndFields[k].split(" ");
                    if (f.getName().equals(conditions.length > 1 ? conditions[1] : conditions[0])) {
                        j--;
                        continue for1;//let for1 continue;
                    }
                }
                f.setAccessible(true);//set the field can be visit
                stringBuffer.append(f.getName());
                stringBuffer.append(" = ? ");
                if (i < fs.length - 1)
                    stringBuffer.append(", ");
                objects[j] = f.get(t);
                objectsPosition = j + 1;
            }
            stringBuffer.append(" where ");
            for (int i = 0, m = objectsPosition; i < conditionAndFields.length; i++, m++) {
                stringBuffer.append(conditionAndFields[i]);
                stringBuffer.append(" = ? ");
                for (int j = 0; j < fs.length; j++) {
                    Field f = fs[j];
                    f.setAccessible(true);//set the field can be visit
                    String[] conditions = conditionAndFields[i].split(" ");
                    if (f.getName().equals(conditions.length > 1 ? conditions[1] : conditions[0])) {
                        objects[m] = f.get(t);
                    }
                }
            }
            return DBC3P0_Util_DG.ExecuteSql(stringBuffer.toString(), objects);
        } catch (Exception ee) {
            log.info(ee);
            return false;
        }
    }

    /**
     * Like DB_ORM_DG.orm_deleteTable("tb_test",test,"Name") the test is an object
     *
     * @param tableName
     * @param t
     * @param conditionAndFields
     * @param <T>
     * @return
     */
    public static <T> boolean orm_deleteTable(String tableName, T t, String... conditionAndFields) throws Exception {
        try {
            StringBuffer stringBuffer = new StringBuffer();
            stringBuffer.append("delete from ");
            stringBuffer.append(tableName);
            stringBuffer.append(" where ");
            Field[] fs = t.getClass().getDeclaredFields();
            Object[] objects = new Object[conditionAndFields.length];
            for (int i = 0; i < conditionAndFields.length; i++) {
                stringBuffer.append(conditionAndFields[i]);
                stringBuffer.append(" = ? ");
                for (int j = 0; j < fs.length; j++) {
                    Field f = fs[j];
                    f.setAccessible(true);//set the field can be visit
                    String[] conditions = conditionAndFields[i].split(" ");
                    if (f.getName().equals(conditions.length > 1 ? conditions[1] : conditions[0])) {
                        objects[i] = f.get(t);
                    }
                }
            }
            return DBC3P0_Util_DG.ExecuteSql(stringBuffer.toString(), objects);
        } catch (IllegalAccessException e) {
            log.info(e);
            return false;
        }
    }

    /**
     * return data count in table
     *
     * @param tableName
     * @return
     */
    public static int orm_dataCountTable(String tableName) {
        try {
            String sql = "select count(0) from " + tableName;
            return Integer.parseInt(DBC3P0_Util_DG.QuerySingleData(sql).toString());
        } catch (Exception ee) {
            log.info(ee);
            return 0;
        }
    }

    /**
     * return data count in table that have 'where' already
     *
     * @param tableName
     * @param condition
     * @return
     */
    public static int orm_dataCountTable(String tableName, String condition) {
        try {
            String sql = "select count(0) from " + tableName + " where " + condition;
            return Integer.parseInt(DBC3P0_Util_DG.QuerySingleData(sql).toString());
        } catch (Exception ee) {
            log.info(ee);
            return 0;
        }
    }

    /**
     * check the table if exist the data ? if true else false then you can insert
     * Like DB_ORM_DG.orm_isExist("tb_test",test,"Id") the test is an object
     *
     * @param tableName
     * @param t
     * @param conditionAndFields
     * @param <T>
     * @return
     */
    public static <T> boolean orm_isExist(String tableName, T t, String... conditionAndFields) {
        try {
            StringBuffer stringBuffer = new StringBuffer();
            stringBuffer.append("select count(0) from ");
            stringBuffer.append(tableName);
            stringBuffer.append(" where ");
            Field[] fs = t.getClass().getDeclaredFields();
            Object[] objects = new Object[conditionAndFields.length];
            for (int i = 0; i < conditionAndFields.length; i++) {
                stringBuffer.append(conditionAndFields[i]);
                stringBuffer.append(" = ? ");
                for (int j = 0; j < fs.length; j++) {
                    Field f = fs[j];
                    f.setAccessible(true);//set the field can be visit
                    String[] conditions = conditionAndFields[i].split(" ");
                    if (f.getName().equals(conditions.length > 1 ? conditions[1] : conditions[0])) {
                        objects[i] = f.get(t);
                    }
                }
            }
            return Integer.parseInt(DBC3P0_Util_DG.QuerySingleData(stringBuffer.toString(), objects).toString()) > 0 ? true : false;
        } catch (IllegalAccessException e) {
            log.info(e);
            return false;
        } catch (Exception ee) {
            log.info(ee);
            return false;
        }
    }

    /**
     * select * from tb_test where...
     * like: test=DB_ORM_DG.orm_selectTableAll("tb_test",test,"Id");
     *
     * @param tableName
     * @param t
     * @param conditionAndFields
     * @param <T>
     * @return
     */
    public static <T> T orm_selectTableAll(String tableName, T t, String... conditionAndFields) {
        try {
            StringBuffer stringBuffer = new StringBuffer();
            stringBuffer.append("select * from ");
            stringBuffer.append(tableName);
            stringBuffer.append(" where ");
            Field[] fs = t.getClass().getDeclaredFields();
            Object[] objects = new Object[conditionAndFields.length];
            for (int i = 0; i < conditionAndFields.length; i++) {
                stringBuffer.append(conditionAndFields[i]);
                stringBuffer.append(" = ? ");
                for (int j = 0; j < fs.length; j++) {
                    Field f = fs[j];
                    f.setAccessible(true);//set the field can be visit
                    String[] conditions = conditionAndFields[i].split(" ");
                    if (f.getName().equals(conditions.length > 1 ? conditions[1] : conditions[0])) {
                        objects[i] = f.get(t);
                    }
                }
            }
            return (T) DBC3P0_Util_DG.QuerySingleLine(t.getClass(), stringBuffer.toString(), objects);
        } catch (IllegalAccessException e) {
            log.info(e);
            return null;
        } catch (Exception ee) {
            log.info(ee);
            return null;
        }
    }

    /**
     * select Id,Name from tb_test where...
     * info:like    String [] strings={"Id","Name"};
     * test=DB_ORM_DG.orm_selectTableFields("tb_test",strings,test,"Id");
     *
     * @param tableName
     * @param selectedFields
     * @param t
     * @param conditionAndFields
     * @param <T>
     * @return
     */
    public static <T> T orm_selectTableFields(String tableName, String[] selectedFields, T t, String... conditionAndFields) {
        try {
            StringBuffer stringBuffer = new StringBuffer();
            stringBuffer.append("select ");
            for (int i = 0; i < selectedFields.length; i++) {
                stringBuffer.append(selectedFields[i]);
                if (i < selectedFields.length - 1)
                    stringBuffer.append(",");
            }
            stringBuffer.append(" from ");
            stringBuffer.append(tableName);
            stringBuffer.append(" where ");
            Field[] fs = t.getClass().getDeclaredFields();
            Object[] objects = new Object[conditionAndFields.length];
            for (int i = 0; i < conditionAndFields.length; i++) {
                stringBuffer.append(conditionAndFields[i]);
                stringBuffer.append(" = ? ");
                for (int j = 0; j < fs.length; j++) {
                    Field f = fs[j];
                    f.setAccessible(true);//set the field can be visit
                    String[] conditions = conditionAndFields[i].split(" ");
                    if (f.getName().equals(conditions.length > 1 ? conditions[1] : conditions[0])) {
                        objects[i] = f.get(t);
                    }
                }
            }
            System.out.println(stringBuffer.toString());
            return (T) DBC3P0_Util_DG.QuerySingleLine(t.getClass(), stringBuffer.toString(), objects);
        } catch (IllegalAccessException e) {
            log.info(e);
            return null;
        } catch (Exception ee) {
            log.info(ee);
            return null;
        }
    }
}
