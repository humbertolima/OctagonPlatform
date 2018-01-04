$(function() {

    Morris.Area({
        element: 'cew-transactions-chart',
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
        lineColors: ['gold'],
        fillOpacity: '0.5',
        resize: true
    });

    Morris.Bar({
        element: 'cew-transactionsbyTerminal-chart',
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
        element: 'cew-transactionsbyCity-chart',
        data: [
            { y: 'South Florida', a: 30000 },
            { y: 'Los Amgeles', a: 10000 },
            { y: 'New York', a: 13450 },
            { y: 'Atlanta', a: 7500 },
            { y: 'Chicago', a: 8000 },
            { y: 'Buenos Aires', a: 20000 },
            { y: 'Rosario', a: 24000 },
            { y: 'Stgo de Chile', a: 5000 },
            { y: 'Mexico DF', a: 9000 }
        ],
        xLabelAngle: 35,
        xkey: 'y',
        ykeys: ['a'],
        labels: ['Transactions'],
        hideHover: 'auto',
        resize: true
    });
    
});
