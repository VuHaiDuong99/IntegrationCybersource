$(document).ready(function () {
    // Khởi tạo đối tượng cổng thanh toán
    new GatewayJS();
    marqueeInit({
        uniqueid: 'bankscroll',
        style: {
            'padding': '2px',
            'width': '790px',
            'height': '36px'
        },
        inc: 1,
        mouse: 'cursor driven', //mouseover behavior ('pause' 'cursor driven' or false)
        moveatleast: 1,
        neutral: 150,
        savedirection: true,
        random: false
    });
})

/**===========================================
 * Class quản lý sự kiện cho trang gateway
 * CreatedBy: NDBINH (19/01/2021)
 * */
class GatewayJS {
    constructor() {
        this.initInforOrder();
        this.initCardYear();
        this.initCardMonth();
        this.showAtmCardName();
        this.showAtmCardNumber();
        this.showAtmCardExp();
        this.initEventsOfGateway();
    }

    /**
     * Khởi tạo sự kiện của trang cổng thanh toán
     * CreatedBy: NDBINH (19/02/2021)
     * */
    initEventsOfGateway() {
        let thisInIt = this;
        // Kiểm tra giá trị input bắt buộc nhập
        $('input[required]').blur(function () {
            thisInIt.eventsValidateRequiredWhenInputBlur(this);
        })

        // Thực hiện thanh toán khi nhấn nút thanh toán
        $('#g-btn-pay').click(this.eventWhenClickButtonPay.bind(thisInIt));

        // Thực hiện chuyển trang khi giao dịch thành công
        $('#click-here').click(this.evenWhenClickHere.bind(thisInIt));

        // Thực hiện xác thực thanh toán khi nhấn nút thanh toán
        //$('.g-btn-pay-success').click(this.evenwhenclickbuttonpaysuccess.bind(thisinit));
    }

    /**
     * Khởi tạo giá trị cho combobox năm
     * CreatedBy: NDBINH (19/02/2021)
     * */
    initCardYear() {
        let cardYear = $("#cardYear");
        cardYear.empty();
        for (var i = 0; i < 100; i++) {
            if (i < 10)
                cardYear.append(`<option value ='0${i}'>0${i}</option>`);
            else
                cardYear.append(`<option value ='${i}'>${i}</option>`)
        }
    }

    /**
     * Khởi tạo giá trị cho combobox tháng
     * CreatedBy: NDBINH (19/02/2021)
     * */
    initCardMonth() {
        let cardMonth = $("#cardMonth");
        cardMonth.empty();
        for (var i = 0; i < 13; i++) {
            if (i < 10)
                cardMonth.append(`<option value='0${i}'>0${i}</option>`)
            else
                cardMonth.append(`<option value='${i}'>${i}</option>`)
        }
    }
    /**
     * Sự kiện khi focus vào thẻ nhập tên chủ thẻ
     * CreatedBy: NDBINH (21/02/2021)
     * */
    showAtmCardName() {
        var cardName = $('#txtTenChuThe');
        cardName.focus(function () {
            $('#imgCardLogo').attr('src', '/content/img/cardName.jpg')
            $('.logo-desc').text('Vui lòng gõ tiếng Việt không dấu, chữ hoa hoặc chữ thường: "Nguyen Van A".');
        });
    }

    /**
     * Sự kiện khi focus vào thẻ nhập số thẻ
     * CreatedBy: NDBINH (21/02/2021)
     * */
    showAtmCardNumber() {
        var cardNumber = $('#txtSoThe');
        cardNumber.focus(function () {
            $('#imgCardLogo').attr('src', '/content/img/cardNumber.jpg');
            $('.logo-desc').text('Nhập số thẻ liên tục, không sử dụng dấu cách: "9704360000000000056".');
        });
    }

    /**
     * Sự kiện khi focus vào thẻ tháng/năm
     * CreatedBy: NDBINH (21/02/2021)
     * */
    showAtmCardExp() {
        var cardExpDate = $('.cardExpDate');
        cardExpDate.focus(function () {
            $('#imgCardLogo').attr('src', '/content/img/cardExpdate.jpg');
            $('.logo-desc').text('Chọn thông tin tháng/năm phát hành hoặc hết hạn in trên mặt trước của thẻ: "04/11".');
        });
    }

    /**
     * Kiểm tra trường bắt buộc nhập
     * @param {any} seft đại diện cho đối tượng input, select
     * CreatedBy: NDBINH (21/02/2021)
     */
    eventsValidateRequiredWhenInputBlur(seft) {
        let inScope = seft;
        let value = $(inScope).val();
        if (!value) {
            $(inScope).addClass('border-red');
            $(inScope).attr('title', `${$(inScope).attr('name')} không được để trống`);
            $(inScope).attr('validated', false);
        }
        else {
            $(inScope).removeClass('border-red');
            $(inScope).attr('validated', true);
        }
    }

    /**
     * Thực hiện thanh toán
     * CreatedBy: NDBINH (21/02/2021)
     * */
    eventWhenClickButtonPay() {
        let thisHere = this;
        $('.error-message').text('');
        // validate dữ liệu
        let inputValidates = $('input[required]');
        $.each(inputValidates, function (index, input) {
            $(input).trigger('blur');
        })
        // Lấy các thẻ input có thuộc tính validated = false
        let inputNotValidates = $('input[validated = "false"]');
        if (inputNotValidates && inputNotValidates.length > 0) {
            // Thông báo không hợp lệ dữ liệu
            $('.error-message').append(`<img style="margin-right:6px; margin-top:2px;" src="/content/icon/error_icon.gif" />
                            <div>Qúy khách vui lòng nhập đầy đủ thông tin.</div>`);
            inputNotValidates[0].focus();
            return;
        }
        else {
            if (thisHere.FormMode == 'pay2') {
                var codeOTP = '0000';
                var codeCaptcha = 'UAE';
                if ($('#txtMaOTP').val() == codeOTP) {
                    if ($('#txtMaCaptcha').val() == codeCaptcha) {
                        $('.payment-method').empty();
                        $('.payment-method').append(`<div style="text-align:center; height: 306px; width:100%; font-family:Tahoma; font-size:14px; padding-top:20px; color: #519fcc">
                        <span class="font-bold" style="color: #0A75C3;">Giao dịch thanh toán thành công.</span> <br />Cảm ơn quý khách đã sử dụng dịch vụ. Qúy khách sẽ được quay về trang của
                        Đơn vị chấp nhận thẻ để tiếp tục giao dịch.<br /> Trong trường hợp không chuyển được, Qúy khách vui lòng bấm <a id="click-here" style="color:blue; text-decoration:underline;" class="font-bold">VÀO ĐÂY</a> để biết thêm chi tiết
                    </div>`);
                        this.initEventsOfGateway();
                        setTimeout(function () {
                            window.location = window.location.protocol +
                                '//' + window.location.hostname + ':5001' + `/home/ordersuccess?order=1852925295&payment_processing=0`;
                        }, 2000);
                    }
                    else {
                        $('.error-message').append(`<img style="margin-right:6px; margin-top:2px;" src="/content/icon/error_icon.gif" />
                            <div>Qúy khách vui lòng nhập chính xác chuỗi ký tự hiển thị bên dưới.</div>`);
                    }
                }
                else {
                    $('.error-message').append(`<img style="margin-right:6px; margin-top:2px;" src="/content/icon/error_icon.gif" />
                            <div>Qúy khách nhập sai mật khẩu OTP, vui lòng nhập lại.</div>`);
                }
                return;
            }
            let inforInput = $('.method-input');
            inforInput.empty();
            inforInput.append(`<div class="g-same-row">
                                <div class="g-lable">Chọn ngân hàng ::</div>
                                <div class="form-input"><input type="text" name="Tên ngân hàng" value="VPBank" id="txtTenNganHang" readonly /></div>
                            </div>
                            <div class="g-same-row">
                                <div class="g-lable">Nhập OTP ::</div>
                                <div class="form-input"><input type="text" name="Mã OTP" id="txtMaOTP" required style="width: 70px;" /></div>
                            </div>
                            <div class="g-same-row">
                                <div class="g-lable">Nhập mã sau ::</div>
                                <div class="form-input"><input type="text" name="Mã captcha" id="txtMaCaptcha" required style="width: 70px;" /></div>
                                <img src="/content/img/democaptcha.png" />
                            </div>`);

            $('#imgCardLogo').attr('src', '');
            $('.logo-desc').text('');
            $('.tutorial-security').empty();
            this.FormMode = 'pay2';
            this.initEventsOfGateway();
        }
    }

    /**
     * Lấy thông tin đơn hàng
     * CreatedBy: NDBINH (22/02/2021)
     * */
    initInforOrder() {
        var tongTien = GetParameterValues('tongtien');
        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }
        $('#txtGiaTriDonHang').val(tongTien + ' VND');
    }

    /**
     * Thực hiện chuyển sang merchant
     * CreatedBy: NDBINH (24/02/2021)
     * */
    evenWhenClickHere() {
        window.location = window.location.protocol +
            '//' + window.location.hostname + ':5001' + `/home/ordersuccess`;
    }
}