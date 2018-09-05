package QX_JFrame.Bantina;//package QX_JFrame.Bantina;
//
//import org.json.JSONArray;
//import org.json.JSONObject;
//import java.io.IOException;
//import java.io.InputStream;
//import java.lang.reflect.Field;
//import java.sql.*;
//import java.util.ArrayList;
//import java.util.List;
//import java.util.Properties;
//
///**
// * Author:qixiao
// * Time:2016年9月2日23:47:08
// */
//public class DB_Util_DG {
//    /* the class can be extends */
//
//    // 数据库驱动类名称
//    private static String driver_class = null;            //"com.mysql.jdbc.Driver";
//    // 连接字符串
//    private static String driver_url = null;                //"jdbc:mysql://localhost:3306/db_liuyanban";
//    // 用户名
//    private static String database_user = null;                //"root";
//    // 密码
//    private static String database_password = null;            // "root";
//    // 创建数据库连接对象
//    private static Connection connnection = null;
//    // 创建结果集对象
//    private static ResultSet resultSet = null;
//    // 创建PreparedStatement对象
//    private static PreparedStatement preparedStatement = null;
//    // 创建CallableStatement对象
//    private static CallableStatement callableStatement = null;
//
//    static {
//        try {
//            Properties properties = new Properties();
//            InputStream fis = DB_Util_DG.class.getClassLoader().getResourceAsStream("jdbc.properties");// 加载数据库配置文件到内存中放在同src下
//            try {
//                properties.load(fis);// 获取数据库配置文件
//                driver_class = properties.getProperty("driver_class").toString();
//                driver_url = properties.getProperty("driver_url").toString();
//                database_user = properties.getProperty("database_user").toString();
//                database_password = properties.getProperty("database_password").toString();
//
//                System.out.println("数据库配置文件读取成功！driver_class=" + driver_class + ",driver_url=" + driver_url + "database_user=" + database_user + "database_password=" + database_password);
//                // 获得连接
//                connnection = DriverManager.getConnection(driver_url, database_user, database_password);
//
//            } catch (IOException e) {
//                e.printStackTrace();
//            } catch (SQLException e) {
//                e.printStackTrace();
//            }
//            Class.forName(driver_class);  // 加载数据库驱动程序
//        } catch (ClassNotFoundException e) {
//            System.out.println("加载驱动错误");
//            System.out.println(e.getMessage());
//        }
//    }
//
//    /**
//     * 所有执行方法的辅助器(共同参与的部分)
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     */
//    private static void SqlPrepareCommand(String sql, Object... params) {
//        try {
//            // 调用SQL
//            preparedStatement = connnection.prepareStatement(sql);
//            // 参数赋值
//            if (params != null) {
//                for (int i = 0; i < params.length; i++) {
//                    preparedStatement.setObject(i + 1, params[i]);
//                }
//            }
//        } catch (Exception e) {
//            System.out.println(e.getMessage());
//        }
//    }
//
//    /**
//     * insert update delete SQL语句的执行的统一方法
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     * @return 受影响的行数
//     */
//    public static boolean executeUpdate(String sql, Object... params) {
//        try {
//            SqlPrepareCommand(sql, params);//调用通用的初始化方法来设置参数,声明事务手动执行
//            return preparedStatement.executeUpdate() > 0;// 执行
//        } catch (SQLException e) {
//            System.out.println(e.getMessage());
//            return false;
//        } finally {
//            // Dispose
//            closeAll();
//        }
//    }
//
//    /**
//     * SQL 查询将查询结果直接放入ResultSet中
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     * @return 结果集
//     */
//    public static ResultSet executeQueryResultSet(String sql, Object... params) {
//        try {
//            SqlPrepareCommand(sql, params);                //调用通用的初始化方法来设置参数
//            return preparedStatement.executeQuery();    // 执行
//        } catch (SQLException e) {
//            System.out.println(e.getMessage());
//            return null;
//        }
//    }
//
//    /**
//     * SQL 查询将查询结果：一行一列 (左上角的结果)
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     * @return 结果集
//     */
//    public static Object executeQuerySingleData(String sql, Object... params) {
//        try {
//            resultSet = executeQueryResultSet(sql, params);
//            return resultSet.next() ? resultSet.getObject(1) : null;
//        } catch (SQLException e) {
//            System.out.println(e.getMessage());
//            return null;
//        } finally {
//            closeAll();
//        }
//    }
//
//    /**
//     * SQL 查询将查询结果：一行 返回class类型的对象(第一行结果)
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     * @return 结果集
//     */
//    public static <T> T executeQuerySingleLine(Class<T> clazz, String sql, Object... params) {
//        try {
//            resultSet = executeQueryResultSet(sql, params);                                //从数据库获得结果集
//            if (resultSet != null) {
//                ResultSetMetaData rsmd = resultSet.getMetaData();                        // 获得结果集的MetaData
//                T t = clazz.newInstance();                                                // 通过反射创建 T 实例
//                while (resultSet.next()) {
//                    for (int i = 1; i <= rsmd.getColumnCount(); i++) {
//                        Field tField = clazz.getDeclaredField(rsmd.getColumnLabel(i));    //获取到单行记录对应的字段
//                        tField.setAccessible(true);                                        //设置通过反射访问该Field时取消访问权限检查
//                        tField.set(t, resultSet.getObject(rsmd.getColumnLabel(i)));        //获取到每一行结果对应字段的值并赋值给类属性
//                    }
//                }
//                return t;                                                                //返回赋值后的T对象
//            } else return null;
//        } catch (Exception e) {
//            return null;
//        }
//    }
//
//    /**
//     * 获取结果集，并将结果放在List中生成List<T> (多行数据)
//     *
//     * @param clazz 类
//     * @param sql   SQL语句
//     * @params List结果集
//     */
//    public static <T> List<T> executeQueryList(Class<T> clazz, String sql, Object... params) {
//        try {
//            resultSet = executeQueryResultSet(sql, params);                                //从数据库获得结果集
//            List<T> list = new ArrayList<T>();                                        //实例化一个泛型List<T>保存结果的List
//            try {
//                ResultSetMetaData rsmd = resultSet.getMetaData();                        // 获得结果集的MetaData
//                while (resultSet.next()) {
//                    T t = clazz.newInstance();                                            // 通过反射创建 clazz 实例
//                    for (int i = 1; i <= rsmd.getColumnCount(); i++) {
//                        try {
//                            Field tField = clazz.getDeclaredField(rsmd.getColumnLabel(i));    //获取到单行记录对应的字段
//                            tField.setAccessible(true);                                        //设置通过反射访问该Field时取消访问权限检查
//                            tField.set(t, resultSet.getObject(rsmd.getColumnLabel(i)));        //获取到每一行结果对应字段的值并赋值给类属性
//                        } catch (Exception e) {
//                            continue;
//                        }
//                    }
//                    list.add(t);                                                        //将实例化赋值的对象存到List<T>中
//                }
//            } catch (Exception e) {
//                System.out.println(e.getMessage());
//                return null;
//            }
//            return list;//返回填充对象的list
//        } finally {
//            closeAll();//调用自定义的关闭所有资源方法关闭所有资源
//        }
//    }
//
//    /**
//     * 存储过程带有一个输出参数的方法
//     *
//     * @param sql         存储过程语句
//     * @param params      参数数组
//     * @param outParamPos 输出参数位置
//     * @param SqlType     输出参数类型
//     */
//    public static Object executeQueryProcedure(String sql, Object[] params, int outParamPos, int SqlType) {
//        Object object = null;
//        try {
//            connnection = DriverManager.getConnection(driver_url, database_user, database_password);
//            // 调用存储过程
//            callableStatement = connnection.prepareCall(sql);
//            // 给参数赋值
//            if (params != null) {
//                for (int i = 0; i < params.length; i++) {
//                    callableStatement.setObject(i + 1, params[i]);
//                }
//            }
//            // 注册输出参数
//            callableStatement.registerOutParameter(outParamPos, SqlType);
//            // 执行
//            callableStatement.execute();
//            // 得到输出参数
//            object = callableStatement.getObject(outParamPos);
//
//        } catch (SQLException e) {
//            System.out.println(e.getMessage());
//        } finally {
//            // 释放资源
//            closeAll();
//        }
//        return object;
//    }
//
//    /**
//     * 将查询到的单行数据转换成JsonObject
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     * @return JSONObject结果
//     */
//    public static JSONObject executeQuerySingleLineToJsonObject(String sql, Object... params) {
//        try {
//            resultSet = executeQueryResultSet(sql, params);                                //从数据库获得结果集
//            JSONObject jsonObject = new JSONObject();
//            if (resultSet != null) {
//                ResultSetMetaData rsmd = resultSet.getMetaData();                        // 获得结果集的MetaData
//                while (resultSet.next()) {
//                    for (int i = 1; i <= rsmd.getColumnCount(); i++) {
//                        jsonObject.put(rsmd.getColumnLabel(i), resultSet.getObject(rsmd.getColumnLabel(i)));
//                    }
//                }
//                return jsonObject;                                                                //返回赋值后的T对象
//            } else return null;
//        } catch (Exception e) {
//            return null;
//        }
//    }
//
//    /**
//     * 将ResultSet数据转换成JsonArray
//     *
//     * @param sql    SQL语句
//     * @param params 参数数组，若没有参数则为null
//     * @return JSONArray结果集
//     */
//    public static JSONArray executeQueryResultSetToJsonArray(String sql, Object... params) {
//        try {
//            resultSet = executeQueryResultSet(sql, params);
//            JSONArray jsonArray = new JSONArray();
//            if (resultSet != null) {
//                ResultSetMetaData rsmd = resultSet.getMetaData();// 获得结果集的MetaData
//                while (resultSet.next()) {
//                    JSONObject jsonMember = new JSONObject();
//                    for (int i = 1; i <= rsmd.getColumnCount(); i++) {
//                        jsonMember.put(rsmd.getColumnLabel(i), resultSet.getObject(rsmd.getColumnLabel(i)));
//                    }
//                    jsonArray.put(jsonMember);
//                }
//                return jsonArray;//返回赋值后的T对象
//            } else return null;
//        } catch (Exception e) {
//            System.err.println(e.getMessage());
//            return null;
//        }
//    }
//
//    /**
//     * 关闭所有资源
//     */
//    private static void closeAll() {
//        // 关闭结果集对象
//        if (resultSet != null) {
//            try {
//                resultSet.close();
//            } catch (SQLException e) {
//                System.out.println(e.getMessage());
//            }
//        }
//        // 关闭PreparedStatement对象
//        if (preparedStatement != null) {
//            try {
//                preparedStatement.close();
//            } catch (SQLException e) {
//                System.out.println(e.getMessage());
//            }
//        }
//        // 关闭CallableStatement 对象
//        if (callableStatement != null) {
//            try {
//                callableStatement.close();
//            } catch (SQLException e) {
//                System.out.println(e.getMessage());
//            }
//        }
//        // 关闭Connection 对象
//        if (connnection != null) {
//            try {
//                connnection.close();
//            } catch (SQLException e) {
//                System.out.println(e.getMessage());
//            }
//        }
//    }
//}
