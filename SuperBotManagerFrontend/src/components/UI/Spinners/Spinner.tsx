import React from 'react';

import { Spin, SpinProps } from 'antd';
import { useTranslation } from 'react-i18next';
import classes from './Spinner.module.css';
interface SpinnerProps extends SpinProps {
	box?: boolean;
}

const Spinner: React.FC<SpinnerProps> = ({
	box = false,
	tip: propTip,
	size,
	children,
	...props
}) => {
	const loadingTranslation = useTranslation().t('Loading');
	const tip = propTip ?? loadingTranslation;
	return (
		<Spin
			{...props}
			tip={tip}
			size={size ?? 'large'}
			style={{
				position: 'absolute',
				zIndex: '999',
				width: '100%',
				height: '100%',
				alignItems: 'center',
				paddingTop: '20%',
				backgroundColor: 'rgba(0,0,0,0.1)',
				maxHeight: 'none'
			}}
			wrapperClassName={classes.SpinnerWrapper}
		>
			{children ?? <div></div>}
		</Spin>
	);
};

export default Spinner;
