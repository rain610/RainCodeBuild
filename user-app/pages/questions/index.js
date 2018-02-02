const requestUrl = require('../../config/url.js');
var tmp = [];
var checkTmp = {
    '8':[],
    '9': [],
    '10': [],
    '11': [],
    '12': [],
    '13': [],
    '14': [],
    '15': [],
}
Page({
    data:{
        radioArr:[],
        checkArr:[],
        questionResultContent:''
    },
    radioChange(e){
        var val = e.detail.value;
        if (tmp[val.split(':')[0]-1]){
            tmp[val.split(':')[0]-1] = val;
        }else{
            tmp.push(val);
        }
        this.setData({
            radioArr: tmp
        })
    },
    checkboxChange(e){
        checkTmp[e.target.id] = e.detail.value;
        this.setData({
            checkArr: checkTmp
        })
    },
    bindSubQues(){
        var that = this;
        console.log(this.data.radioArr)
        console.log(this.data.checkArr)
        var str = '';
        str += this.data.radioArr.join('|')+'|';
        Object.keys(that.data.checkArr).map(function(item,index){
            str += that.data.checkArr[item].join('|')+'|'
        })
        console.log(str);
        this.setData({
            questionResultContent:str
        })
        wx.request({
            url:requestUrl.host.dev + requestUrl.url.SubmitQuestionSurvey,
            methods:"GET",
            data:{
                guestBloodID:getApp().globalData.guestBloodID,
                questionResultContent:this.data.questionResultContent
            },
            success:(data)=>{
                var result = data.data;
                if(result.IsSuccess){
                    wx.showToast({
                        title:'提交成功',
                        success:()=>{
                            setTimeout(function(){
                                wx.hideToast();
                                wx.redirectTo({
                                    url: '../../pages/success/index',
                                })
                            },2000)
                        }
                    })
                }else{
                    wx.showToast({
                        title:'提交失败',
                        success:()=>{
                            setTimeout(function(){
                                wx.hideToast();
                            },2000)
                        }
                    })
                }
            }
        })
        
    }
})