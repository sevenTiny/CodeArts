/*********************************************************
 * CopyRight: QIXIAO CODE BUILDER.
 * Version:4.2.0
 * Author:qixiao(柒小)
 * Create:2016-9-2 23:47:08
 * Update:2017-09-01 15:07:58
 * E-mail: dong@qixiao.me | wd8622088@foxmail.com
 * Personal WebSit: http://qixiao.me
 * Technical WebSit: http://www.cnblogs.com/qixiaoyizhan/
 * Description:-.
 * Thx , Best Regards ~
 *********************************************************/
package QX_JFrame.Bantina;

import QX_JFrame.Bantina.Bankinate.DB_ReadAndWrite;
import QX_JFrame.Bantina.Bankinate.DataBaseType;
import com.mchange.v2.c3p0.ComboPooledDataSource;
import org.jetbrains.annotations.Nullable;

import java.beans.PropertyVetoException;
import java.io.IOException;
import java.io.InputStream;
import java.lang.reflect.Field;
import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;
import java.util.UUID;

public abstract class DBC3P0_Util_DG {
    //jdbc.properties src
    private static String dbProperties = "jdbc.properties";
    //db type DataBaseType.Mysql default
    private static DataBaseType dbType = DataBaseType.Mysql;
    //ComboPooledDataSource db connection pull
    private static ComboPooledDataSource cpds = null;
    // Connection
    private static Connection connection = null;
    // ResultSet
    private static ResultSet resultSet = null;
    // PreparedStatement
    private static PreparedStatement preparedStatement = null;
    // CallableStatement
    private static CallableStatement callableStatement = null;

    //constructor
    protected DBC3P0_Util_DG() {
    }

    // base operation ---

    /**
     * InitDBC3P0_Util_DG
     *
     * @param db_readAndWrite
     */
    private static void InitDBC3P0_Util_DG(DB_ReadAndWrite db_readAndWrite) throws SQLException, IOException, PropertyVetoException {
            Properties properties = new Properties();
            InputStream fis = DBC3P0_Util_DG.class.getClassLoader().getResourceAsStream(dbProperties);// 加载数据库配置文件到内存中放在同src下
                properties.load(fis);// 获取数据库配置文件
                System.out.println("start read jdbc.properties !");
                // settings of ComboPooledDataSource
                cpds = new ComboPooledDataSource();
                cpds.setDriverClass(properties.getProperty("jdbc.driverClassName"));
                cpds.setJdbcUrl(properties.getProperty("jdbc.url"));
                cpds.setUser(properties.getProperty("jdbc.username"));
                cpds.setPassword(properties.getProperty("jdbc.password"));
                //#c3p0连接池信息
                cpds.setMinPoolSize(Integer.parseInt(properties.getProperty("c3p0.minPoolSize")));
                cpds.setMaxPoolSize(Integer.parseInt(properties.getProperty("c3p0.maxPoolSize")));
                //#当连接池中的连接耗尽的时候c3p0一次同时获取的连接数
                cpds.setAcquireIncrement(Integer.parseInt(properties.getProperty("c3p0.acquireIncrement")));
                //#定义在从数据库获取新连接失败后重复尝试的次数
                cpds.setAcquireRetryAttempts(Integer.parseInt(properties.getProperty("c3p0.acquireRetryAttempts")));
                //#两次连接中间隔时间，单位毫秒
                cpds.setAcquireRetryDelay(Integer.parseInt(properties.getProperty("c3p0.acquireRetryDelay")));
                //#连接关闭时默认将所有未提交的操作回滚
                cpds.setAutoCommitOnClose(Boolean.getBoolean(properties.getProperty("c3p0.autoCommitOnClose")));
                //#当连接池用完时客户端调用getConnection()后等待获取新连接的时间，超时后将抛出SQLException,如设为0则无限
                cpds.setCheckoutTimeout(Integer.parseInt(properties.getProperty("c3p0.checkoutTimeout")));
                //#每120秒检查所有连接池中的空闲连接。Default: 0
                cpds.setIdleConnectionTestPeriod(Integer.parseInt(properties.getProperty("c3p0.idleConnectionTestPeriod")));
                //#最大空闲时间,60秒内未使用则连接被丢弃。若为0则永不丢弃。Default: 0
                cpds.setMaxIdleTime(Integer.parseInt(properties.getProperty("c3p0.maxIdleTime")));
                //#如果设为true那么在取得连接的同时将校验连接的有效性。Default: false
                cpds.setTestConnectionOnCheckin(Boolean.getBoolean(properties.getProperty("c3p0.testConnectionOnCheckin")));
                //#如果maxStatements与maxStatementsPerConnection均为0，则缓存被关闭。Default: 0
                cpds.setMaxStatements(Integer.parseInt(properties.getProperty("c3p0.maxStatements")));
                //#maxStatementsPerConnection定义了连接池内单个连接所拥有的最大缓存statements数。Default: 0
                cpds.setMaxStatementsPerConnection(Integer.parseInt(properties.getProperty("c3p0.maxStatementsPerConnection")));
                //#自动超时回收Connection
                cpds.setUnreturnedConnectionTimeout(Integer.parseInt(properties.getProperty("c3p0.unreturnedConnectionTimeout")));

                System.out.println("Read jdbc.properties success ! --- QX_JFrame.Bantina");
                connection = cpds.getConnection();
    }

    /**
     * GetConneciton
     *
     * @return connectin
     */
    private static Connection GetConneciton(DB_ReadAndWrite db_readAndWrite) throws SQLException, IOException, PropertyVetoException {
        if (connection == null || connection.isClosed()) {
            if (cpds == null)
                InitDBC3P0_Util_DG(db_readAndWrite);
            return cpds.getConnection();
        }
        return connection;
    }

    /**
     * shared method
     *
     * @param sql    SQL语句
     * @param params 参数数组，若没有参数则为null
     */
    private static void SqlPrepareCommand(DB_ReadAndWrite db_readAndWrite, String sql, Object... params) throws Exception {
        //get connection
        connection = GetConneciton(db_readAndWrite);
        //execute sql
        preparedStatement = connection.prepareStatement(sql);
        // 参数赋值
        if (params != null) {
            for (int i = 0; i < params.length; i++) {
                preparedStatement.setObject(i + 1, params[i]);
            }
        }
    }

    /**
     * DisposeAll
     */
    private static void DisposeAll() throws SQLException {
        // 关闭结果集对象
        if (resultSet != null)
            resultSet.close();
        // 关闭PreparedStatement对象
        if (preparedStatement != null)
            preparedStatement.close();
        // 关闭CallableStatement 对象
        if (callableStatement != null)
            callableStatement.close();
        // 关闭Connection 对象
        if (connection != null)
            connection.close();
    }

    // base operation --- end

    /**
     * insert update delete SQL语句的执行的统一方法
     *
     * @param sql    SQL语句
     * @param params 参数数组，若没有参数则为null
     * @return 受影响的行数
     */
    public static boolean ExecuteSql(String sql, Object... params) throws Exception {
        try {
            SqlPrepareCommand(DB_ReadAndWrite.Write, sql, params);
            return preparedStatement.executeUpdate() > 0;
        } finally {
            DisposeAll();
        }
    }

    /**
     * 存储过程带有一个输出参数的方法
     *
     * @param sql         存储过程语句
     * @param params      参数数组
     * @param outParamPos 输出参数位置
     * @param SqlType     输出参数类型
     */
    public static Object ExecuteProcedure(String sql, Object[] params, int outParamPos, int SqlType) throws Exception {
        try {
            connection = cpds.getConnection();
            callableStatement = connection.prepareCall(sql);// 调用存储过程
            if (params != null)
                for (int i = 0; i < params.length; i++)
                    callableStatement.setObject(i + 1, params[i]);
            callableStatement.registerOutParameter(outParamPos, SqlType);// 注册输出参数
            callableStatement.execute();
            return callableStatement.getObject(outParamPos); // 得到输出参数
        } finally {
            DisposeAll();
        }
    }

    /**
     * Transaction Support
     *
     * @param runnable
     * @throws SQLException
     */
    public static void Transaction(Runnable runnable) throws Exception {
        if (connection == null)
            GetConneciton(DB_ReadAndWrite.Write);
        connection.setAutoCommit(false);// 更改JDBC事务的默认提交方式
        try {
            runnable.run();//runnable execute
            connection.commit();
        } catch (Exception e) {
            connection.rollback();
            throw e;
        } finally {
            connection.setAutoCommit(true);// 恢复JDBC事务的默认提交方式
            DisposeAll();
        }
    }

    /**
     * SQL 查询将查询结果直接放入ResultSet中
     *
     * @param sql    SQL语句
     * @param params 参数数组，若没有参数则为null
     * @return 结果集
     */
    private static ResultSet QueryResultSet(String sql, Object... params) throws Exception {
        SqlPrepareCommand(DB_ReadAndWrite.Read, sql, params);
        return preparedStatement.executeQuery();
    }

    /**
     * SQL 查询将查询结果：一行一列 (左上角的结果)
     *
     * @param sql    SQL语句
     * @param params 参数数组，若没有参数则为null
     * @return 结果集
     */
    @Nullable
    public static Object QuerySingleData(String sql, Object... params) throws Exception {
        try {
            resultSet = QueryResultSet(sql, params);
            return resultSet.next() ? resultSet.getObject(1) : null;
        } finally {
            DisposeAll();
        }
    }

    /**
     * SQL 查询将查询结果：一行 返回class类型的对象(第一行结果)
     *
     * @param sql    SQL语句
     * @param params 参数数组，若没有参数则为null
     * @return 结果集
     */
    @Nullable
    public static <T> T QuerySingleLine(Class<T> clazz, String sql, Object... params) throws Exception {
        List<T> list = QueryList(clazz, sql, params);
        return list.isEmpty() ? null : list.get(0);
    }

    /**
     * 获取结果集，并将结果放在List中生成List<T> (多行数据)
     *
     * @param clazz 类
     * @param sql   SQL语句
     * @params List结果集
     */
    public static <T> List<T> QueryList(Class<T> clazz, String sql, Object... params) throws Exception {
        resultSet = QueryResultSet(sql, params); //get result_set
        List<T> list = new ArrayList<>();//instance a list
        try {
            ResultSetMetaData rsmd = resultSet.getMetaData();//get MetaData
            while (resultSet.next()) {
                T t = clazz.newInstance();//create an instance by reflaction
                for (int i = 1; i <= rsmd.getColumnCount(); i++) {
                    try {
                        Field tField = clazz.getDeclaredField(rsmd.getColumnLabel(i)); //get corresponding feild
                        tField.setAccessible(true); //Cancel the access check cancel access check
                        //check property type
                        Object obj = resultSet.getObject(rsmd.getColumnLabel(i));
                        if (tField.getType().getTypeName().equals(UUID.class.getTypeName())) {
                            byte[] bytes = (byte[]) obj;
                            tField.set(t, UUID.fromString(new String(bytes)));
                        } else {
                            tField.set(t, obj);//Get and set Value
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
                list.add(t);
            }
            return list;
        } finally {
            DisposeAll();//调用自定义的关闭所有资源方法关闭所有资源
        }
    }
}
