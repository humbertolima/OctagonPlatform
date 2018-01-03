$(function() {

    Morris.Area({
        element: 'vendbot-transactions-chart',
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
            period: '2017 Q4',
            transactions: 8432
        }],
        xkey: 'period',
        ykeys: ['transactions'],
        labels: ['Transactions'],
        pointSize: 2,
        hideHover: 'auto',
        lineColors: ['#0580ff'],
        fillOpacity: '0.5',
        resize: true
    });

    Morris.Bar({
        element: 'vendbot-transactions-location-chart',
        data: [{
            y: '2015 Q2',
            transactions: 100
        }, {
            y: '2015 Q4',
            transactions: 75
        }, {
            y: '2016 Q2',
            transactions: 50
        }, {
            y: '2016 Q4',
            transactions: 75
        }, {
            y: '2017 Q2',
            transactions: 50
        }, {
            y: '2017 Q4',
            transactions: 75
        }],
        xkey: 'y',
        ykeys: ['transactions'],
        labels: ['Transactions'],
        hideHover: 'auto',
        resize: true
    });

    Morris.Bar({
        element: 'vendbot-product-sales-chart',
        data: [{
            y: '2015 Q2',
            sales: 100
        }, {
            y: '2015 Q4',
            sales: 75
        }, {
            y: '2016 Q2',
            sales: 50
        }, {
            y: '2016 Q4',
            sales: 75
        }, {
            y: '2017 Q2',
            sales: 50
        }, {
            y: '2017 Q4',
            sales: 75
        }],
        xkey: 'y',
        ykeys: ['sales'],
        labels: ['Sales'],
        hideHover: 'auto',
        resize: true
    });
    
});
