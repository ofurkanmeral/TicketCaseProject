$(document).ready(function () {

    $('.js-example-basic-single').select2({
        width: "100%"
    });

    moment.locale('tr');

    $('input[name="TravelDate"]').daterangepicker({
        singleDatePicker: true,
        locale: {
            format: "D MMMM dddd", // "3 Mart Pazar" şeklinde
            separator: " - ",
            applyLabel: "Uygula",
            cancelLabel: "Vazgeç",
            fromLabel: "Dan",
            toLabel: "a",
            customRangeLabel: "Seç",
            daysOfWeek: ["Pt", "Sl", "Çr", "Pr", "Cm", "Ct", "Pz"],
            monthNames: [
                "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"
            ],
            firstDay: 1
        }
    });
    //bugune set
    $(document).on('click', '.today', function () {
        var today = moment().format("D MMMM dddd");
        $('input[name="TravelDate"]').data('daterangepicker').setStartDate(today);
    });
    //yarına set
    $(document).on('click', '.tomorrow', function () {
        var tomorrow = moment().add(1, 'days').format("D MMMM dddd");
        $('input[name="TravelDate"]').data('daterangepicker').setStartDate(tomorrow);
    });

    // bunuda listeleme sayfasında verileri aktarmak için kullandım
    $("form").submit(function () {
        $("#OriginName").val($(".origin option:selected").text().trim());
        $("#DestinationName").val($(".destination option:selected").text().trim());
    });

    // şehir degistir icon buton
    $(document).on('click', '.changeCity', function () {
        var origin = $('.origin').val();
        var destination = $('.destination').val();
        var temp = origin;

        // T anında aynı olma sorunu giderildi
        $('.origin').val("").trigger('change');
        $('.destination').val(temp).trigger('change');
        $('.origin').val(destination).trigger('change');
    });

    // LocalStorage Kullanımı (Gidiş ve Dönüş Şehirlerini Saklama)
    if (localStorage.getItem("locOrigin")) {
        $(".origin").val(localStorage.getItem("locOrigin"));
    } else {
        localStorage.setItem("locOrigin", $('.origin').val());
    }

    if (localStorage.getItem("locDest")) {
        $(".destination").val(localStorage.getItem("locDest"));
    } else {
        localStorage.setItem("locDest", $('.destination').val());
    }

    $(document).on('click', '.searchButton', function () {
        var origin = $('.origin').val();
        var destination = $('.destination').val();
        var date = $('input[name="TravelDate"]').data('daterangepicker').startDate.format('YYYY-MM-DD');

        var searchParam = {
            Origin: origin,
            Destination: destination,
            TravelDate: date
        };

        $.ajax({
            url: "/Home/ListModel",
            method: 'post',
            contentType: "application/json",
            data: JSON.stringify(searchParam),
            success: function () {
            },
            error: function () {
                console.log("Bir Hata ile Karşılaşıldı");
            }
        });
    });

    // gidiş- dönüş şehirlerin aynı olmasını Engelleme
    $(document).on("change", ".origin, .destination", function () {
        var origin = $('.origin').val();
        var destination = $('.destination').val();

        if (origin === destination) {
            $(this).val("").trigger("change");
            Swal.fire({
                icon: "error",
                title: "Oops...",
                text: "Gidiş Dönüş Aynı İstikamet Seçilmez!",
            });
        }
    });
});
