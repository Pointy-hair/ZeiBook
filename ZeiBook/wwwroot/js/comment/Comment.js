/**
 * Created by Administrator on 2017/4/8.
 */

class Comment {

    static GetComment(id,content,userName,postTime,userId) {
        return {
            id,
            content,
            userName,
            postTime,
            userId
        }
    }

    static GetComments(array1){
        var tempArray = [];
        array1.forEach((item)=> {
           tempArray.push(this.GetComment(item.id,item.content,item.userName,item.postTime,item.userId));
        });
        return tempArray;
    }

    constructor(content) {
        this.id = Math.random();
        this.content = content;
        this.userName = "mei";
        this.postTime = "2017-4-8 12:12";
    }
}

export default Comment;