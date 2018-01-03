$(function() {

    Morris.Area({
        element: 'demmoney-area-chart',
        data: [{
            period: '2015 Q1',
            Investment: 2666
        }, {
            period: '2015 Q2',
            Investment: 2778
        }, {
            period: '2015 Q3',
            Investment: 4912
        }, {
            period: '2015 Q4',
            Investment: 3767
        }, {
            period: '2016 Q1',
            Investment: 6810
        }, {
            period: '2016 Q2',
            Investment: 5670
        }, {
            period: '2016 Q3',
            Investment: 4820
        }, {
            period: '2016 Q4',
            Investment: 15073
        }, {
            period: '2017 Q1',
            Investment: 10687
        }, {
            period: '2017 Q4',
            Investment: 8432
        }],
        xkey: 'period',
        ykeys: ['Investment'],
        labels: ['Investment'],
        pointSize: 2,
        hideHover: 'auto',
        lineColors: ['#0580ff'],
        fillOpacity: '0.5',
        resize: true
    });

    Morris.Donut({
        element: 'demmoney-donut-chart',
        data: [{
            label: "Buy",
            value: 207
        }, {
            label: "Sell",
            value: 240
        }, {
            label: "Total",
            value: 447
        }],
        colors: ["Green", "Red", "Blue"],
        resize: true
    });

    Morris.Bar({
        element: 'demmoney-bar-chart',
        data: [{
            y: '2015 Q2',
            a: 100
        }, {
            y: '2015 Q4',
            a: 75
        }, {
            y: '2016 Q2',
            a: 50
        }, {
            y: '2016 Q4',
            a: 75
        }, {
            y: '2017 Q2',
            a: 50
        }, {
            y: '2017 Q4',
            a: 75
        }],
        xkey: 'y',
        ykeys: ['a'],
        labels: ['Revenue'],
        hideHover: 'auto',
        resize: true
    });
    
});
