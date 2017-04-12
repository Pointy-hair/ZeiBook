/**
 * Created by Administrator on 2017/4/12.
 */
import React from 'react';

class PageItem extends React.Component {

    constructor() {
        super();
        this.handlerPage = this.handlerPage.bind(this);
    }

    handlerPage(event) {
        this.props.changePage(this.props.value);
    }

    render() {
        return <li onClick={this.handlerPage}>
            <a href="javascript:void(0)">{this.props.value}</a>
        </li>;
    }
}

export default PageItem;
