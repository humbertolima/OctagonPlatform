$(function () {

    Morris.Area({
        element: 'dashboard-transactions-chart',
        data: [{
            period: '2015 Q1',
            transactions: 2666
        }, {
            period: '2015 Q2',
            transactions: 2778
        }, {
            period: '2015 Q3',
            transactions: 4912
        }, {
            period: '2015 Q4',
            transactions: 3767
        }, {
            period: '2016 Q1',
            transactions: 6810
        }, {
            period: '2016 Q2',
            transactions: 5670
        }, {
            period: '2016 Q3',
            transactions: 4820
        }, {
            period: '2016 Q4',
            transactions: 15073
        }, {
            period: '2017 Q1',
            transactions: 10687
        }, {
            period: '2017 Q2',
            transactions: 8432
        }, {
            period: '2017 Q3',
            transactions: 18432
        }, {
            period: '2017 Q4',
            transactions: 20432
        }],
        xkey: 'period',
        ykeys: ['transactions'],
        labels: ['Transactions'],
        pointSize: 2,
        fillOpacity: 0.5,
        hideHover: 'auto',
        resize: true
    });

    Morris.Area({
        element: 'dashboard-cash-load-chart',
        data: [{
            period: '2015 Q1',
            loads: 666
        }, {
            period: '2015 Q2',
            loads: 778
        }, {
            period: '2015 Q3',
            loads: 912
        }, {
            period: '2015 Q4',
            loads: 767
        }, {
            period: '2016 Q1',
            loads: 1810
        }, {
            period: '2016 Q2',
            loads: 1670
        }, {
            period: '2016 Q3',
            loads: 820
        }, {
            period: '2016 Q4',
            loads: 1073
        }, {
            period: '2017 Q1',
            loads: 2687
        }, {
            period: '2017 Q2',
            loads: 3432
        }, {
            period: '2017 Q3',
            loads: 2432
        }, {
            period: '2017 Q4',
            loads: 3432
        }],
        xkey: 'period',
        ykeys: ['loads'],
        labels: ['Loads'],
        pointSize: 2,
        lineColors: ['green'],
        fillOpacity: 0.5,
        hideHover: 'auto',
        resize: true
    });

    Morris.Area({
        element: 'dashboard-surcharges-chart',
        data: [{
            period: '2015 Q1',
            loads: 666
        }, {
            period: '2015 Q2',
            loads: 778
        }, {
            period: '2015 Q3',
            loads: 912
        }, {
            period: '2015 Q4',
            loads: 767
        }, {
            period: '2016 Q1',
            loads: 1810
        }, {
            period: '2016 Q2',
            loads: 1670
        }, {
            period: '2016 Q3',
            loads: 820
        }, {
            period: '2016 Q4',
            loads: 1073
        }, {
            period: '2017 Q1',
            loads: 1687
        }, {
            period: '2017 Q2',
            loads: 1489
        }, {
            period: '2017 Q3',
            loads: 1845
        }, {
            period: '2017 Q4',
            loads: 1432
        }],
        xkey: 'period',
        ykeys: ['loads'],
        labels: ['Loads'],
        pointSize: 2,
        fillOpacity: 0.5,
        hideHover: 'auto',
        resize: true
    });

    Morris.Bar({
        element: 'dashboard-interxchange-chart',
        data: [{
            period: '2015 Q1',
            loads: 166
        }, {
            period: '2015 Q2',
            loads: 378
        }, {
            period: '2015 Q3',
            loads: 212
        }, {
            period: '2015 Q4',
            loads: 467
        }, {
            period: '2016 Q1',
            loads: 510
        }, {
            period: '2016 Q2',
            loads: 370
        }, {
            period: '2016 Q3',
            loads: 420
        }, {
            period: '2016 Q4',
            loads: 573
        }, {
            period: '2017 Q1',
            loads: 487
        }, {
            period: '2017 Q2',
            loads: 432
        }, {
            period: '2017 Q3',
            loads: 332
        }, {
            period: '2017 Q4',
            loads: 522
        }],
        xkey: 'period',
        ykeys: ['loads'],
        labels: ['Loads'],
        hideHover: 'auto',
        barColors: ['green'],
        resize: true
    });

});
