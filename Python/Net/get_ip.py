import socket

def get_host_ip():
    """
    查询本机ip地址
    :return: ip
    """
    try:
        s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        s.connect(('8.8.8.8', 80))
        ip = s.getsockname()[0]
    finally:
        s.close()
        return ip

if __name__ == '__main__':
    # 获取计算机名
    hostname = socket.gethostname()
    # 获取本机ip
    ip = get_host_ip()

    print(ip)