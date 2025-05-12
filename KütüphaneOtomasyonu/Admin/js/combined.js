AOS.init({
    duration: 800,
    easing: 'ease-in-out',
    once: true
});

$(document).ready(function() {
    $('.sidebar-toggle').click(function() {
        $('.sidebar').toggleClass('show');
    });

    $(document).click(function(e) {
        if ($(window).width() <= 768) {
            if (!$(e.target).closest('.sidebar, .sidebar-toggle').length) {
                $('.sidebar').removeClass('show');
            }
        }
    });
}); 