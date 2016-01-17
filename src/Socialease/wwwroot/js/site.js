// site.js
(function() {
    var ele = $('#username');
    ele.text('Serge Nevsky');
    ele.on('mouseenter', function() {
        ele.style = 'color: green';
    });
    ele.on('mouseleave', function() {
        ele.style = '';
    });
})();
