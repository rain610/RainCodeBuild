const requestUrl = require('../../config/url.js');
const regex = require('../../config/regex.js');

Page({
    data:{
        sessionID:''
    },
    bindsessionId(e){
        this.setData({
            sessionID:e.detail.value
        })
    },
    bindlogin(){
        if (regex.number(this.data.sessionID)){
            wx.request({
                url:requestUrl.host.dev + requestUrl.url.IsValidSessionID,
                method:'GET',
                data:{
                    sessionID:this.data.sessionID
                },
                success:(data)=>{
                    var result = data.data;
                    if(!result.IsSuccess){
                        wx.showToast({
                            title: result.Message,
                            success:()=>{
                                setTimeout(function () {
                                    wx.hideToast()
                                }, 2000)
                            }
                        });
                    }else{
                        getApp().globalData.bloodID = result.BloodID;
                        if(result.CheckItem != ''){
                            getApp().globalData.checkItem_1 = result.CheckItem.indexOf('1') != -1? 1 : '';
                            getApp().globalData.checkItem_2 = result.CheckItem.indexOf('2') !=-1 ? 2 : '';
                        }
                        wx.redirectTo({
                            url:'../../pages/index/index'
                        })
                    }
                }
            })
        }else{
            wx.showToast({
                title: '请输入有效场次编号',
                success:()=>{
                    setTimeout(function () {
                        wx.hideToast()
                    }, 2000)
                }
            });
        }
    },
    bindInfoDetail(){
        wx.redirectTo({
            url:'../../pages/infodetail/index?back=true'
        })
    }
})