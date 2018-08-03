import * as React from 'react';
import { NavMenu } from './NavMenu';

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return <div className='container-fluid noborder'>
            <div className='row'>
                <NavMenu />
            </div>
            <div className='row custom_content content'>
                {this.props.children}
            </div>
        </div>;
    }
    
}
