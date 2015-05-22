$("#priceOrder").click(function () {
    order("price");
})
var order = function (orderByWhat) {
    if (window.orderBy == orderByWhat) {
        $("#priceIcon").removeClass("fa-angle-down");
        $("#priceIcon").addClass("fa-angle-up");
        window.orderBy = orderByWhat + "-des";
    } else {
        $("#priceIcon").removeClass("fa-angle-up");
        $("#priceIcon").addClass("fa-angle-down");
        window.orderBy = orderByWhat;
    }
    $.ajax({
        url: "/Home/GetDataOrdered",
        type: "GET",
        data: { simtype: window.simtype, network: window.network, price_min: window.price_min, price_max: window.price_max, searchStr: window.searchStr, orderBy: window.orderBy, page: window.page, numCount: window.numCount, pageSize: window.pageSize },
        success: function (data) {
            var result = "";
            var link = "";
            $.each(data.Items, function (index) {
                var sim = data.Items[index];
                var price = new String(sim.Price);
                link = convertUnicode(sim.Name);
                sim.SimType = sim.SimType == null ? "" : sim.SimType;
                var x1 = '<div class="col-md-1 col-xs-6 sm-hidden"><a href="#">' + ((page - 1) * 20 + index + 1) + '</a></div>';
                var x2 = '<div class="col-md-3 col-xs-6 bold"><a href="/dat-mua-sim/' + sim.Number + '">' + sim.Number + '</a></div>';
                var x3 = '<div class="col-md-2 col-xs-4"><span>' + price.moneyFormat(",") + '</span></div>';
                var x4 = '<div class="col-md-2 sm-hidden text-center"><a href="/mang/' + link + '"><img src="' + sim.image + '" alt="' + sim.Name + '+" width="30" height="18"></a></div>';
                var x5 = '<div class="col-md-2 sm-hidden text-center"><span>' + sim.SimType + '</span></div>';
                var x6 = '<div class="col-md-2 col-xs-2 text-center"><a href="/dat-mua-sim/' + sim.Number + '" class="inline btn-addtocard"><i class="fa fa-shopping-cart"></i><span class="inline sm-hidden">Mua sim</span></a></div>';
                result += '<li class="dataDynamic">' + x1 + x2 + x3 + x4 + x5 + x6 + '</li>';
            })
            $(".dataDynamic").remove();
            $("#dataTable").append(result);

            $(".pagination").children("a").each(function (index) {
                var href = new String($(this).attr("href"));
                if (href.indexOf("orderBy") < 0) {
                    href += "&orderBy=" + window.orderBy;
                    $(this).attr("href", href);
                } else {
                    href = href.substr(0, href.indexOf("orderBy"));
                    href += "orderBy=" + window.orderBy;
                    $(this).attr("href", href);
                }
            });
        },
        error: function (err) {
            alert(err);
        }
    })
}
var convertUnicode = function (str) {
    if (str) {
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, 'a');
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, 'e');
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, 'i');
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, 'o');
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, 'u');
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, 'y');
        str = str.replace(/đ/g, 'd');
        str = str.replace(' ', '-');
    }
    return str;
};