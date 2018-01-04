$(function() {

    Morris.Area({
        element: 'transactions-chart',
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

    Morris.Bar({
        element: 'transactions-terminal-chart',
        data: [{
            period: '2015 Q1',
            transactions: 666
        }, {
            period: '2015 Q2',
            transactions: 778
        }, {
            period: '2015 Q3',
            transactions: 912
        }, {
            period: '2015 Q4',
            transactions: 1767
        }, {
            period: '2016 Q1',
            transactions: 1810
        }, {
            period: '2016 Q2',
            transactions: 1670
        }, {
            period: '2016 Q3',
            transactions: 1820
        }, {
            period: '2016 Q4',
            transactions: 5073
        }, {
            period: '2017 Q1',
            transactions: 4687
        }, {
            period: '2017 Q2',
            transactions: 3432
        }, {
            period: '2017 Q3',
            transactions: 8432
        }, {
            period: '2017 Q4',
            transactions: 14432
        }],
        xkey: 'period',
        ykeys: ['transactions'],
        labels: ['Transactions'],
        hideHover: 'auto',
        resize: true
    });

    Morris.Area({
        element: 'cash-load-chart',
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

    Morris.Bar({
        element: 'cash-loads-terminal-chart',
        data: [{
            period: '2015 Q1',
            loads: 166
        }, {
            period: '2015 Q2',
            loads: 578
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
            loads: 820
        }, {
            period: '2016 Q4',
            loads: 1073
        }, {
            period: '2017 Q1',
            loads: 1687
        }, {
            period: '2017 Q2',
            loads: 932
        }, {
            period: '2017 Q3',
            loads: 1432
        }, {
            period: '2017 Q4',
            loads: 1522
        }],
        xkey: 'period',
        ykeys: ['loads'],
        labels: ['Loads'],
        hideHover: 'auto',
        barColors: ['green'],
        resize: true
    });

    Morris.Area({
        element: 'surcharges-chart',
        data: [{
            period: '2015 Q1',
            surcharges: 666
        }, {
            period: '2015 Q2',
            surcharges: 778
        }, {
            period: '2015 Q3',
            surcharges: 912
        }, {
            period: '2015 Q4',
            surcharges: 767
        }, {
            period: '2016 Q1',
            surcharges: 1810
        }, {
            period: '2016 Q2',
            surcharges: 1670
        }, {
            period: '2016 Q3',
            surcharges: 820
        }, {
            period: '2016 Q4',
            surcharges: 1073
        }, {
            period: '2017 Q1',
            surcharges: 1687
        }, {
            period: '2017 Q2',
            surcharges: 1489
        }, {
            period: '2017 Q3',
            surcharges: 1845
        }, {
            period: '2017 Q4',
            surcharges: 1432
        }],
        xkey: 'period',
        ykeys: ['surcharges'],
        labels: ['Surcharges Total'],
        pointSize: 2,
        lineColors: ['grey'],
        fillOpacity: 0.5,
        hideHover: 'auto',
        resize: true
    });

    Morris.Bar({
        element: 'interxchange-chart',
        data: [{
            period: '2015 Q1',
            interxchange: 166
        }, {
            period: '2015 Q2',
            interxchange: 378
        }, {
            period: '2015 Q3',
            interxchange: 212
        }, {
            period: '2015 Q4',
            interxchange: 467
        }, {
            period: '2016 Q1',
            interxchange: 510
        }, {
            period: '2016 Q2',
            interxchange: 370
        }, {
            period: '2016 Q3',
            interxchange: 420
        }, {
            period: '2016 Q4',
            interxchange: 573
        }, {
            period: '2017 Q1',
            interxchange: 487
        }, {
            period: '2017 Q2',
            interxchange: 432
        }, {
            period: '2017 Q3',
            interxchange: 332
        }, {
            period: '2017 Q4',
            interxchange: 522
        }],
        xkey: 'period',
        ykeys: ['interxchange'],
        labels: ['InterXchange Total'],
        hideHover: 'auto',
        barColors: ['grey'],
        resize: true
    });

    Morris.Area({
        element: 'alerts-chart',
        data: [{
            period: '2015 Q1',
            network: 2666,
            lowCash: 1620,
            outOfOrder: 2647
        }, {
            period: '2015 Q2',
            network: 2778,
            lowCash: 2294,
            outOfOrder: 2441
        }, {
            period: '2015 Q3',
            network: 4912,
            lowCash: 1969,
            outOfOrder: 2501
        }, {
            period: '2015 Q4',
            network: 3767,
            lowCash: 3597,
            outOfOrder: 5689
        }, {
            period: '2016 Q1',
            network: 6810,
            lowCash: 1914,
            outOfOrder: 2293
        }, {
            period: '2016 Q2',
            network: 5670,
            lowCash: 4293,
            outOfOrder: 1881
        }, {
            period: '2016 Q3',
            network: 4820,
            lowCash: 3795,
            outOfOrder: 1588
        }, {
            period: '2016 Q4',
            network: 15073,
            lowCash: 5967,
            outOfOrder: 5175
        }, {
            period: '2017 Q1',
            network: 10687,
            lowCash: 4460,
            outOfOrder: 2028
        }, {
            period: '2017 Q2',
            network: 8432,
            lowCash: 5713,
            outOfOrder: 1791
        }, {
            period: '2017 Q3',
            network: 10687,
            lowCash: 4460,
            outOfOrder: 2028
        }, {
            period: '2017 Q4',
            network: 8432,
            lowCash: 5713,
            outOfOrder: 1791
        }],
        xkey: 'period',
        ykeys: ['network', 'lowCash', 'outOfOrder'],
        labels: ['Network Issue', 'Low Cash', 'Out of Order'],
        pointSize: 2,
        fillOpacity: 0.5,
        hideHover: 'auto',
        resize: true
    });
    
});
