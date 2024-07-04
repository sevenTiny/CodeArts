## javascript客户端获取完整的stream json返回值示例
> 这并不是最佳实践，只是一个示例用来获取完整的stream json返回值
> 正常情况下，我们应该获取到stream后，直接处理stream，拿到每次返回的字符进行业务上的实时显示

``` javascript
const getFromStream = function (response) {
        if (response.code == 200) {
            var chunk = response.stream;
            var text = '';

            for (var i = 0; i < chunk.byteLength; i++) {
                text += String.fromCharCode(chunk[i]);
            }

            text = text.replaceAll('data:', '');

            const textArray = text.split('[END]')

            const json = {
                Message: {
                    Role: '',
                    Content: ''
                },
                FinishReason: ''
            };

            for (item in textArray) {
                const current = textArray[item]?.trim();
                if (!current || current == '')
                    continue;

                try {
                    var obj = JSON.parse(current);
                    if (obj) {
                        if ('Message' in obj) {
                            const msg = obj['Message'];
                            if (msg) {
                                if ('Role' in msg && msg['Role'] != '') {
                                    json.Message.Role = msg['Role'];
                                }
                                if ('Content' in msg) {
                                    json.Message.Content = json.Message.Content + msg['Content'];
                                }
                            }
                        }

                        if ('FinishReason' in obj) {
                            json['FinishReason'] = obj['FinishReason'];
                        }

                        if ('Model' in obj && obj['Model'] != '') {
                            json['Model'] = obj['Model'];
                        }
                    }
                } catch (e) {
                    console.error(e);
                }
            }

            return {
                code: response.code,
                data: json
            };
        } else {
            const resJson = response.json();
            return {
                code: resJson['code'],
                message: resJson['message']
            };
        }
    }

    const jsonData = getFromStream(pm.response);
    //var jsonData = pm.response.json();
    // console.log("jsonData=",jsonData)
```