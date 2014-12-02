
class KLine {
    constructor() {
        this.timedata = [];
        this.pricedata = [];
    }
    title: string;
    legend: string;
    timedata: string[];
    pricedata : Object[];

    GetData(jsondata: Object[]) {
        var count = jsondata.length;
        this.title = jsondata[0]["Name"];
        this.legend = jsondata[0]["Name"];
        var temp = [];

        for (var i = 0; i < count; i++) {
            this.timedata.push(jsondata[i]["Date"]);

            temp.push(jsondata[i]["OpenPrice"]);
            temp.push(jsondata[i]["ClosePrice"]);
            temp.push(jsondata[i]["LowerPrice"]);
            temp.push(jsondata[i]["HeightPrice"]);

            this.pricedata.push(temp);
        }
        
    }

}

