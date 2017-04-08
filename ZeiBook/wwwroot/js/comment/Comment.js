/**
 * Created by Administrator on 2017/4/8.
 */

class Comment {

    constructor(content) {
        this.id = Math.random();
        this.content = content;
        this.userName = "mei";
        this.postTime = "2017-4-8 12:12";
    }
}

export default Comment;