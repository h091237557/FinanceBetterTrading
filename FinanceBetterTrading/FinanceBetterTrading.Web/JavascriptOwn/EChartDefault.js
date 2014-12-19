


var echartDefault = (function () {
    function echartDefault(echartRequrie) {
        debugger;
        this.title ="";
        this.price =[];
        this.date = [];
        this.reqirepath = ['echarts', 'echarts/chart/k'];
        this.echartqeuire = echartRequrie;
    }

    echartDefault.prototype.defaultOption = function (target) {
        var that = this;
        this.echartqeuire.config({
            paths: {
                echarts: '../Scripts/Echart/dist'
            }
        });
        this.echartqeuire(this.reqirepath, function (ec) {
            
            var myChart = ec.init(target);
            var option = {
                title: {
                    text: that.title
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: function (params) {
                        var res = params[0].seriesName + ' ' + params[0].name;
                        res += '<br/>  開盤 : ' + params[0].value[0] + '  最高 : ' + params[0].value[3];
                        res += '<br/>  收盤 : ' + params[0].value[1] + '  最低 : ' + params[0].value[2];
                        return res;
                    }
                },
                legend: {
                    data: [that.title]
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataZoom: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                dataZoom: {
                    show: true,
                    realtime: true,
                    start: 50,
                    end: 100
                },
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: true,
                        axisTick: { onGap: false },
                        splitLine: { show: false },
                        data: that.date
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        scale: true,
                        boundaryGap: [0.01, 0.01]
                    }
                ],
                series: [
                    {
                        name: that.title,
                        type: 'k',
                        data: that.price
                    }
                ]
            };

            myChart.setOption(option);
        });

     
 
    };

    return echartDefault;

  

})();


