(function ($) {
    "use strict";

    new WOW().init();

    //navbar cart
    $(".cart_link > a").on("click", function () {
        $(".mini_cart").addClass("active");
    });

    $(".mini_cart_close > a").on("click", function () {
        $(".mini_cart").removeClass("active");
    });

    //sticky navbar
    $(window).on("scroll", function () {
        var scroll = $(window).scrollTop();
        if (scroll < 100) {
            $(".sticky-header").removeClass("sticky");
        } else {
            $(".sticky-header").addClass("sticky");
        }
    });

    // background image
    function dataBackgroundImage() {
        $("[data-bgimg]").each(function () {
            var bgImgUrl = $(this).data("bgimg");
            $(this).css({
                "background-image": "url(" + bgImgUrl + ")", // concatenation
            });
        });
    }

    $(window).on("load", function () {
        dataBackgroundImage();
    });

    //for carousel slider of the slider section
    $(".slider_area").owlCarousel({
        animateOut: "fadeOut",
        autoplay: true,
        loop: true,
        nav: false,
        autoplayTimeout: 6000,
        items: 1,
        dots: true,
    });

    //product column responsive
    $(".product_column3").slick({
        centerMode: true,
        centerPadding: "0",
        slidesToShow: 5,
        arrows: true,
        rows: 2,
        prevArrow:
            '<button class="prev_arrow"><i class="ion-chevron-left"></i></button>',
        nextArrow:
            '<button class="next_arrow"><i class="ion-chevron-right"></i></button>',
        responsive: [
            {
                breakpoints: 400,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                },
            },
            {
                breakpoints: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2,
                },
            },
            {
                breakpoints: 992,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                },
            },
            {
                breakpoints: 1200,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                },
            },
        ],
    });

    //for tooltip
    $('[data-toggle="tooltip"]').tooltip();

    //tooltip active
    $(".action_links ul li a, .quick_button a").tooltip({
        animated: "fade",
        placement: "top",
        container: "body",
    });

    //product row activation responsive
    $(".product_row1").slick({
        centerMode: true,
        centerPadding: "0",
        slidesToShow: 5,
        arrows: true,
        prevArrow:
            '<button class="prev_arrow"><i class="ion-chevron-left"></i></button>',
        nextArrow:
            '<button class="next_arrow"><i class="ion-chevron-right"></i></button>',
        responsive: [
            {
                breakpoints: 400,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1,
                },
            },
            {
                breakpoints: 768,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2,
                },
            },
            {
                breakpoints: 992,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                },
            },
            {
                breakpoints: 1200,
                settings: {
                    slidesToShow: 4,
                    slidesToScroll: 4,
                },
            },
        ],
    });

    // blog section
    $(".blog_column3").owlCarousel({
        autoplay: true,
        loop: true,
        nav: true,
        autoplayTimeout: 5000,
        items: 3,
        dots: false,
        margin: 30,
        navText: [
            '<i class="ion-chevron-left"></i>',
            '<i class="ion-chevron-right"></i>',
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            768: {
                items: 2,
            },
            992: {
                items: 3,
            },
        },
    });

    //navactive responsive
    $(".product_navactive").owlCarousel({
        autoplay: false,
        loop: true,
        nav: true,
        items: 4,
        dots: false,
        navText: [
            '<i class="ion-chevron-left arrow-left"></i>',  
            '<i class="ion-chevron-right arrow-right"></i>',
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            250: {
                items: 2,
            },
            480: {
                items: 3,
            },
            768: {
                items: 4,
            },
        },
    });

    $(".modal").on("shown.bs.modal", function (e) {
        $(".product_navactive").resize();
    });

    $(".product_navactive a").on("click", function (e) {
        e.preventDefault();
        var $href = $(this).attr("href");
        $(".product_navactive a").removeClass("active");
        $(this).addClass("active");
        $(".product-details-large .tab-pane").removeClass("active show");
        $(".product-details-large " + $href).addClass("active show");
    });
})(jQuery);


document.addEventListener("DOMContentLoaded", () => {
    const showPopupBtn = document.querySelector(".login-btn");
    const formPopup = document.querySelector(".form-popup");
    const hidePopupBtn = document.querySelector(".close-btn");
    const loginSignupLink = document.querySelectorAll(".form-box .bottom-link a");

    showPopupBtn?.addEventListener("click", () => {
        document.body.classList.toggle("show-popup");
    });

    hidePopupBtn?.addEventListener("click", () => {
        document.body.classList.remove("show-popup");
        resetLoginInputs();
    });
    function resetLoginInputs() {
        const loginInputs = document.querySelectorAll('.form-popup .login .input-field input');
        loginInputs.forEach(input => {
            input.value = '';
        });
    }

    loginSignupLink.forEach(link => {
        link.addEventListener("click", (e) => {
            e.preventDefault();
            formPopup.classList[link.id === 'signup-link' ? 'add' : 'remove']("show-signup");
        });
    });
    if (document.querySelector(".form-popup .login-error")) {
        document.body.classList.add("show-popup");
    }
});

// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Activate tooltip
    $('[data-toggle="tooltip"]').tooltip();

    // Select/Deselect checkboxes
    var checkbox = $('table tbody input[type="checkbox"]');
    $("#selectAll").click(function () {
        if (this.checked) {
            checkbox.each(function () {
                this.checked = true;
            });
        } else {
            checkbox.each(function () {
                this.checked = false;
            });
        }
    });
    checkbox.click(function () {
        if (!this.checked) {
            $("#selectAll").prop("checked", false);
        }
    });
});
$(document).ready(function () {
    $(document).on('click', '.quick-view-btn', function (e) {
        e.preventDefault();
        var productId = $(this).data('product-id');
        console.log("Quick View clicked for product ID:", productId);

        // Lấy thông tin sản phẩm
        $.ajax({
            url: '/Home/QuickView',
            type: 'GET',
            data: { id: productId },
            dataType: 'json',
            success: function (product) {
                console.log("Full product data:", product);

                $('#quickview-title').text(product.productName);
                $('#quickview-price').text(product.totalCost.toFixed(2) + ' $');
                $('#quickview-old-price').text((product.totalCost * 1.2).toFixed(2) + ' $');
                $('#quickview-product-id').val(product.productID);

                var img1Src = '/images/product/' + product.imageUrl1;
                var img2Src = '/images/product/' + product.imageUrl2;

                $('#quickview-img-1, #quickview-thumb-1').attr('src', img1Src);
                $('#quickview-img-2, #quickview-thumb-2').attr('src', img2Src);

                // Lấy thông tin gem riêng biệt nếu cần
                getGemByProductId(productId);
            },
            error: function (xhr, status, error) {
                console.error("Ajax error:", status, error);
                console.log("Response text:", xhr.responseText);
            }
        });
    });

    $('#quickview-add-to-cart-form').submit(function (e) {
        e.preventDefault();
        // Implement your add to cart logic here
    });

    // Hàm để lấy thông tin gem dựa vào productId
    function getGemByProductId(productId) {
        $.ajax({
            url: '/Home/GetGemByProductId',
            type: 'GET',
            data: { productId: productId },
            success: function (data) {
                if (data) {
                    // Hiển thị thông tin gem
                    $('#gemInfo').html(`
                        <strong><p style="color: black;">Gem Code: ${data.gemCode}</p></strong>
                        <strong><p style="color: black;">Gem Name: ${data.gemName}</p></strong>
                        <strong><p style="color: black;">Origin: ${data.origin}</p></strong>
                        <strong><p style="color: black;">Proportion: ${data.proportion}</p></strong>
                        <strong><p style="color: black;">Polish: ${data.polish}</p></strong>
                        <strong><p style="color: black;">Symmetry: ${data.symmetry}</p></strong>
                        <strong><p style="color: black;">Fluorescence: ${data.fluorescence}</p></strong>
                    `);

                    // Gọi hàm để lấy thông tin Gem Price List
                    getGemPriceListByProductId(productId);
                } else {
                    $('#gemInfo').html('<p>No gem data found.</p>');
                }
            },
            error: function () {
                $('#gemInfo').html('<p>Error retrieving gem data.</p>');
            }
        });
    }

    function getGemPriceListByProductId(productId) {
        $.ajax({
            url: '/Home/GetGemPriceListByProductId',
            type: 'GET',
            data: { productId: productId },
            dataType: 'json',
            success: function (data) {
                console.log("Gem Price List data received:", data);  // Log the data to check the values
                if (data) {
                    // Hiển thị thông tin Gem Price List
                    $('#gemPriceList').html(`
                    <p style="color: black;">Carat Weight: ${data.caratWeight || 'N/A'}</p>
                    <p style="color: black;">Color: ${data.color || 'N/A'}</p>
                    <p style="color: black;">Clarity: ${data.clarity || 'N/A'}</p>
                    <p style="color: black;">Cut: ${data.cut || 'N/A'}</p>
                `);
                } else {
                    $('#gemPriceList').html('<p>No gem price list data found.</p>');
                }
            },
            error: function (xhr, status, error) {
                console.error("Ajax error:", status, error);
                console.log("Response text:", xhr.responseText);
                $('#gemPriceList').html('<p>Error retrieving gem price list data.</p>');
            }
        });
    }
});

