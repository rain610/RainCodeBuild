const regex = require('../../config/regex.js');
var app = getApp();
Page({
    data:{
        isScan:true,
        isSub:false,
        isWrite:false,
        scanCode:''
    },
    bindscan(){
        wx.scanCode({
            success: (res) => {
                if(res && res.scanType === "CODE_128"){
                    this.setData({
                        scanCode:res.result,
                        isScan:false,
                        isSub:true,
                    })
                }
            }
        })
    },
    bindsubmit(){
        wx.redirectTo({
            url: '../../pages/infodetail/index?scancode=' + this.data.scanCode
        })
        // if (regex.linecode(this.data.scanCode)){
        //     wx.redirectTo({
        //         url: '../../pages/infodetail/index?scancode=DR' + this.data.scanCode
        //     })
        // }else{
        //     wx.showToast({
        //         title: '请输入有条形码',
        //         success: () => {
        //             setTimeout(function () {
        //                 wx.hideToast()
        //             }, 2000)
        //         }
        //     });
        // }
        
    },
    bindwrite(){
        this.setData({
            isScan:false,
            isWrite:true,
        })
    },
    bindCode(e){
        this.setData({
            scanCode:e.detail.value
        })
    }
})