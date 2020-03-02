using System;

namespace Exir.Regate
{
    public static class RegatePriceMaterial
    {
        public static string Build(
            string name = "",
            string label = "",
            decimal? value = null,
            bool isRequired = false,
            bool debug = false,
            bool isToman = false,
            string letterSelector = "")
        {
            var uniqueId = $"RegatePrice__{name.Replace(".", "_")}__{Guid.NewGuid().ToString().Replace("-", "")}";
            var valueString = "";

            if (value != null)
                valueString = string.Format("{0}", value);

            var scriptTag = @"
                <script>
                    function initRegatePrice(targetSelector, isToman, letterSelector) {
                        function commaSeperate(number) {
                            var reversedString = number.split('').reverse().map(function (v, i) {
                                var comma = (i !== 0 && i % 3 === 0) ? ',' : '';
                                return v + comma;
                            });
                            return reversedString.reverse().join('');
                        }

                        function stripComma(number) {
                            var stripped = number.split('').filter(function (v) {
                                return v !== ',';
                            });
                            return stripped.join('');
                        }

                        function removeInvalid(text) {
                            var validCharacters = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹', '۰', ',', '.', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩', '٠'];
                            var validText = text.split('').reduce(function (acc, char) {
                                return acc + (validCharacters.indexOf(char) !== -1 ? char : '');
                            }, '');
                            return validText;
                        }

                        function convertByDict(text, dict) {
                            return text.split('').reduce(function (acc, elem) {
                                return acc + (elem in dict ? dict[elem] || '' : elem)
                            }, '');
                        }

                        function convertToEnglish(text) {
                            var dict = {
                                '۱': '1',
                                '۲': '2',
                                '۳': '3',
                                '۴': '4',
                                '۵': '5',
                                '۶': '6',
                                '۷': '7',
                                '۸': '8',
                                '۹': '9',
                                '۰': '0',
                                '١': '1',
                                '٢': '2',
                                '٣': '3',
                                '٤': '4',
                                '٥': '5',
                                '٦': '6',
                                '٧': '7',
                                '٨': '8',
                                '٩': '9',
                                '٠': '0'
                            }
                            return convertByDict(text, dict);
                        }

                        function convertToPersian(text) {
                            var dict = {
                                '1': '۱',
                                '2': '۲',
                                '3': '۳',
                                '4': '۴',
                                '5': '۵',
                                '6': '۶',
                                '7': '۷',
                                '8': '۸',
                                '9': '۹',
                                '0': '۰',
                                '١': '۱',
                                '٢': '۲',
                                '٣': '۳',
                                '٤': '۴',
                                '٥': '۵',
                                '٦': '۶',
                                '٧': '۷',
                                '٨': '۸',
                                '٩': '۹',
                                '٠': '۰'
                            }
                            return convertByDict(text, dict);
                        }

                        function dotCount(text) {
                            return text.split('').reduce(function (acc, val) {
                                return (val === '.' ? ++acc : acc);
                            }, 0);
                        }

                        function getLastDotPosition(text) {
                            text = String(text);
                            var firstIndex = text.indexOf('.');
                            var lastIndex = text.lastIndexOf('.');

                            return firstIndex;
                        }

                        function removeExtraZero(text) {
                            return String(text).replace(/\..*0+$/, '');
                        }

                        function stripExtraDots(text) {
                            var dotPosition = getLastDotPosition(text);

                            if (dotPosition === -1)
                                return text;

                            return text.split('')
                                .filter(function (val, i) {
                                    return (val === '.' && i === dotPosition) || val !== '.';
                                }).join('');
                        }

                        function tenDivide(text) {
                            text = String(text);
                            text = removeExtraZero(text);
                            text = stripExtraDots(text);
                            var lastDotPosition = getLastDotPosition(text);
                            if (lastDotPosition === -1)
                                return Number(text.replace(/(.)$/, '.$1'));
                            text = text.replace('.', '');
                            return Number([text.slice(0, lastDotPosition - 1), '.', text.slice(lastDotPosition - 1)].join('').replace(/^\./, '0.'));
                        }

                        function tenMultiply(text) {
                            text = String(text);
                            text = removeExtraZero(text);
                            text = stripExtraDots(text);
                            var lastDotPosition = getLastDotPosition(text);
                            if (lastDotPosition === -1)
                                return Number(text + 0);
                            text = text.replace('.', '');
                            return Number([text.slice(0, lastDotPosition + 1), '.', text.slice(lastDotPosition + 1)].join('').replace(/\.$/, ''));
                        }

                        function insertAtLetterSelector(text) {
                            text = text.replace(/\s{2,}/g, ' ').trim();
                            $(letterSelector).val(text).text(text);
                        }

                        var wordifyfa = function (num, level) {
                            'use strict';
                            if (num === null) {
                                return '';
                            }
                            // convert negative number to positive and get wordify value
                            if (num < 0) {
                                num = num * -1;
                                return 'منفی ' + wordifyfa(num, level);
                            }
                            if (num === 0) {
                                if (level === 0) {
                                    return 'صفر';
                                } else {
                                    return '';
                                }
                            }
                            var result = '',
                                yekan = [' یک ', ' دو ', ' سه ', ' چهار ', ' پنج ', ' شش ', ' هفت ', ' هشت ', ' نه '],
                                dahgan = [' بیست ', ' سی ', ' چهل ', ' پنجاه ', ' شصت ', ' هفتاد ', ' هشتاد ', ' نود '],
                                sadgan = [' یکصد ', ' دویست ', ' سیصد ', ' چهارصد ', ' پانصد ', ' ششصد ', ' هفتصد ', ' هشتصد ', ' نهصد '],
                                dah = [' ده ', ' یازده ', ' دوازده ', ' سیزده ', ' چهارده ', ' پانزده ', ' شانزده ', ' هفده ', ' هیجده ', ' نوزده '];
                            if (level > 0) {
                                result += ' و ';
                                level -= 1;
                            }

                            if (num < 10) {
                                result += yekan[num - 1];
                            } else if (num < 20) {
                                result += dah[num - 10];
                            } else if (num < 100) {
                                result += dahgan[parseInt(num / 10, 10) - 2] + wordifyfa(num % 10, level + 1);
                            } else if (num < 1000) {
                                result += sadgan[parseInt(num / 100, 10) - 1] + wordifyfa(num % 100, level + 1);
                            } else if (num < 1000000) {
                                result += wordifyfa(parseInt(num / 1000, 10), level) + ' هزار ' + wordifyfa(num % 1000, level + 1);
                            } else if (num < 1000000000) {
                                result += wordifyfa(parseInt(num / 1000000, 10), level) + ' میلیون ' + wordifyfa(num % 1000000, level + 1);
                            } else if (num < 1000000000000) {
                                result += wordifyfa(parseInt(num / 1000000000, 10), level) + ' میلیارد ' + wordifyfa(num % 1000000000, level + 1);
                            } else if (num < 1000000000000000) {
                                result += wordifyfa(parseInt(num / 1000000000000, 10), level) + ' تریلیارد ' + wordifyfa(num % 1000000000000, level + 1);
                            }
                            return result;

                        };

                        var wordifyRials = function (num) {
                            'use strict';
                            return wordifyfa(num, 0) + ' ریال';
                        };

                        var wordifyRialsInTomans = function (num) {
                            'use strict';
                            if (num >= 10) {
                                num = parseInt(num / 10, 10);
                            } else if (num <= -10) {
                                num = parseInt(num / 10, 10);
                            } else {
                                num = 0;
                            }

                            return wordifyfa(num, 0) + ' تومان';
                        };

                        var $elem = $(targetSelector);
                        if ($elem.data('initRegatePrice') === 'true') {
                            console.log('initRegatePrice is already inited!')
                            return;
                        }

                        $elem.data('initRegatePrice', 'true');

                        var origin = $elem.find('[data-commasep=origin]');
                        var show = $elem.find('[data-commasep=show]');
                        var letterElem = $(letterSelector);

                        if (isToman) {
                            var value = convertToEnglish(show.val());
                            value = tenDivide(value);
                            show.val(value || '');
                        }

                        show.on('input.RegatePrice', function (e) {
                            var value = $(this).val();
                            var showValueArr = [];
                            var floatValue = '';

                            var showValue = removeInvalid(value);
                            showValue = stripComma(showValue);
                            showValue = stripExtraDots(showValue);
                            if (showValue.indexOf('.') !== -1 && showValue.indexOf('.') < showValue.length-1) {
                                showValueArr = showValue.split('.');
                                showValue = showValueArr[0];
                                floatValue = showValueArr[1];
                            }
                            showValue = commaSeperate(showValue);
                            if (floatValue)
                                showValue = String(showValue) + '.' + String(floatValue);
                            showValue = convertToPersian(showValue);
                            show.val(showValue);

                            var originValue = removeInvalid(showValue);
                            originValue = convertToEnglish(originValue);
                            originValue = stripComma(originValue);

                            if (isToman) {
                                originValue = Number(tenMultiply(originValue));
                            }

                            origin.val(originValue || '');

                            if (letterSelector && letterSelector.length) {
                                if (showValueArr.length > 1) {
                                    insertAtLetterSelector([wordifyfa(Number(convertToEnglish(showValueArr[0])), 0), ' ممیز ', wordifyfa(Number(convertToEnglish(showValueArr[1])), 0)].join(''));
                                }
                                else {
                                    insertAtLetterSelector(wordifyfa(originValue, 0));
                                }
                            }
                        });

                        show.on('keydown.RegatePrice', function (e) {
                            var char = e.key;
                            var value = $(this).val();
                            if (e.key === '.' && dotCount(value) > 0) {
                                return false;
                            }
                        });

                        show.trigger('input.RegatePrice');
                    }
                </script>
            ";

            var functionScript = $@"
                <script>
                    initRegatePrice('#{uniqueId}', {isToman.ToString().ToLower()}, '{letterSelector}');
                </script>
            ";

            var markup = $@"
                <span id='{uniqueId}'>
                    <input id='{name}' data-commasep='show' type='text' dir='ltr' style='direction: ltr;' value='{valueString}' class='form-element-field' {(isRequired ? " required='required' " : "")} placeholder='&nbsp;' />
                    <div class='form-element-bar'></div>
                    <label class='form-element-label {(isRequired ? " state--required " : "")}' for='{name}'>{label}</label>
                    <input data-commasep='origin' type='{(debug ? "text" : "hidden")}' name='{name}' value='' class='form-control' />
                </span>
            ";

            return scriptTag + markup + functionScript;
        }
    }
}
