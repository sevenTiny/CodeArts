import datetime
import time
import calendar
'''
time模块，主要处理和时间元组(struct_time)的格式化和解析

格式化主要涉及到2个函数：
strftime：str表示字符串，f是format，time是时间，就是时间格式化为字符串
strptime：str表示字符串，p是parse，time是时间，就是字符串解析为时间

格式	含义
%y	两位数的年份表示，00-99
%Y	四位数的年份表示，0000-9999
%m	月份，01-12
%d	日期，0-31
%H	24小时制小时数，0-23
%I	12小时制小时数，01-12
%M	分钟，00-59
%S	秒，00-59
%a	本地简化星期名称
%A	本地完整星期名称
%b	本地简化的月份名称
%B	本地完整的月份名称
%c	本地日期表示和时间表示
%j	第几天，001-366
%p	本地A.M\P.M
%U	第几周，00-53，星期天为星期的开始
%W	第几周，00-53，星期一为星期的开始
%w	星期几，0-6，星期天为星期的开始
%x	本地日期
%X	本地时间
%Z	当前时区的名称
%%	%符合，因为被当做转义字符
'''


def _time():
    # 格林威治天文时间元组
    # print(time.gmtime())

    # 本地时间元组
    # print(time.localtime())
    # 时间戳转本地时间
    # print(time.localtime(1577851199))

    # 格式化成2020-01-01 11:59:59形式 -> 2022-08-09 10:37:19 (本地时间是动态的)
    print(time.strftime("%Y-%m-%d %H:%M:%S", time.localtime()))

    # 将格式字符串转换为时间元组 -> time.struct_time(tm_year=2020, tm_mon=1, tm_mday=1, tm_hour=11, tm_min=59, tm_sec=59, tm_wday=2, tm_yday=1, tm_isdst=-1)
    print(time.strptime("2020-01-01 11:59:59", "%Y-%m-%d %H:%M:%S"))

    # 时间 -> 时间戳
    # 默认时间戳
    print(time.time())
    # 10位时间戳
    print(int(time.time()))
    # 13位时间戳 (rount 四舍五入)
    print(int(round(time.time() * 1000)))
    # 时间元组到时间戳 -> 1660012697.0
    print(time.mktime(time.localtime()))

    # 时间戳 -> 时间（time元组）
    # 10位时间戳
    print(time.localtime(1660013406))
    # 13位时间戳
    print(time.localtime(1660013406581 / 1000))

    # 休眠秒
    # time.sleep(1)


'''
datetime模块中主要使用的是：
datetime.date：日期(2025-01-01)
datetime.time：时间(12:00:00)
datetime.datetime：时期(2025-01-01 12:00:00)
'''


def _datetime():
    # 当天日期
    today = datetime.date.today()
    print(today)

    # 星期几（isoweekday星期天是第一天，weekday星期一是第一天）
    print(today.isoweekday())
    print(today.weekday())
    # 时间元组
    print(today.timetuple())
    # 第几天
    print(today.toordinal())

    # 现在本地日期
    print(datetime.datetime.now())
    # 现在utc日期
    print(datetime.datetime.now(datetime.timezone.utc))

    # 指定时间
    print(datetime.datetime(2020, 1, 31, 12, 59, 59, 0))

    # 字符串到日期
    print(datetime.datetime.strptime('2020-1-1 12:01', '%Y-%m-%d %H:%M'))
    # 日期格式化
    print(datetime.datetime.now().strftime('%Y-%m-%d %H:%M'))

    # 时间间隔（前一天）
    print(datetime.datetime.now() - datetime.timedelta(1))

    # 上个月最后一天
    lastMonthLastDay = datetime.date(today.year, today.month,
                                     1) - datetime.timedelta(1)
    print(lastMonthLastDay)

    # 上个月第一天
    lastMonthFirstDay = datetime.date(lastMonthLastDay.year,
                                      lastMonthLastDay.month, 1)
    print(lastMonthFirstDay)

    # 一周之前
    print(today - datetime.timedelta(7))

    # 本周一（星期天是第一天）
    thisMonday = today - datetime.timedelta(today.isoweekday() - 1)
    print(thisMonday)

    # 本周星期天
    lastMonday = thisMonday - datetime.timedelta(7)
    print(lastMonday)

    # 上周星期天
    lastSunday = today - datetime.timedelta(days=today.isoweekday())
    print(lastSunday)

    # 上周星期一
    lastMonday = lastSunday - datetime.timedelta(days=6)
    print(lastMonday)

    # 8小时后
    print(datetime.datetime.now() + datetime.timedelta(hours=8))


def _calendar():
    # 打印某年日历
    # print(calendar.calendar(2022))
    # 打印某月日历
    print(calendar.month(2022, 8))
    # 是否是闰年
    print(calendar.isleap(2020))


if __name__ == '__main__':
    _time()
    _datetime()
    # _calendar()