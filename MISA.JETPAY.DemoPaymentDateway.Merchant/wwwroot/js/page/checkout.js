$(document).ready(function () {
    new CheckOutJS();
})
class CheckOutJS {
    constructor() {
        this.initEvent();
    }

    initEvent() {
        let me = this;
        let nameBank = "";
        $(".payment-list").on("click", "li", function () {
            $(this).parent('ul').find('li div').removeClass('border-red');
            $(this).find("div").addClass('border-red');
            // Lấy tên của ngân hàng
            nameBank = $(this).find("div").attr("id");
        })

       // click thanh toán
        $(".btn-access").click(function () {
            $(".payment").css("display", "none");
            $('.option-bank').show();
            $("#nameBank").text(nameBank);
        })
        // sự kiện click đống popup chọn ngân hàng
        $("#btn-close").click(function () {
            $(".payment").css("display", "none");
        })
        // hiển thị ngân hàng được chọn
        $('#ecom-payment').click(function () {

            $(".payment").show();

            /* $('#dialog').hide();*/
        })
        // thay  đổi ngân hàng thanh toán
        $('#ecom-paymentChange').click(function () {
            $(".payment").show();
        })

        // thực hiện thanh toán khi click vao button thanh toán
        $('.btn-pay').click(this.evenWhenClickButtonPay);
        /*let tongtien = $('#txtTongTien').val();
        let nameBank = $('#nameBank').text();*/

    
    }
    /*
     * Hàm chuyển thông tin dữ liệu sang cổng thanh toán
     * createby : vhduong(24/2/2021)
     * *
     */
    evenWhenClickButtonPay() {
        let tongtien = $('#txtTongTien').val();
        let nameBank = $('#nameBank').text();
        // $('.btn-pay button').attr('href', `home/gateway?tongtien=${tongtien}&bank=${nameBank}`);

        window.location = window.location.protocol +
            '//' + window.location.hostname + ':44322' + `/home/gateway?merchantId="Senpay"&id="HD0001"&tongtien=${tongtien}&bank=${nameBank}`;

    }


}