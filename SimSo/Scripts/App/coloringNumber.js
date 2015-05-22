$(document).ready(function () {
    $(".simNumber").each(function (index) {
        var x = window.searchStr;
        var number = $(this);
        var text = number.text();
        if (x.indexOf('*') == -1) {
            number.html(text.replace(x, '<span style="color:red">' + x + '</span>'));
        }
        else {
            var indexOfStar = x.indexOf('*');
            var temp = '';
            while (indexOfStar > -1) {
                if (indexOfStar == 0) {
                    x = x.substring(1);
                    indexOfStar = x.indexOf('*');
                    if (indexOfStar > -1) {
                        var val = x.substring(0, indexOfStar);
                        var indexOfVal = text.indexOf(val);
                        var prefix = text.substring(0, indexOfVal);
                        temp += prefix + '<span style="color:red">' + val + '</span>';
                        text = text.substring(text.indexOf(val) + val.length);
                        x = x.substring(indexOfStar);
                        indexOfStar = 0;
                    } else {
                        temp += text.substring(0, text.length - x.length) + '<span style="color:red">' + x + '</span>';
                    }
                }
                else {
                    var val = x.substring(0, indexOfStar);
                    temp += '<span style="color:red">' + val + '</span>';
                    text = text.substring(val.length);
                    x = x.substring(val.length);
                    indexOfStar = x.indexOf('*');
                }
            }
            number.html(temp);
        }
    });
})
