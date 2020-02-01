import React from 'react';
import PropTypes from 'prop-types';
import { SnackbarProvider, useSnackbar } from 'notistack';

const Toasts = ({ message }) => {
    const { enqueueSnackbar } = useSnackbar();
    if (message) {
        enqueueSnackbar(message.Message, {
            variant: message.variant
        })
    }
}

const ToastsContainer = ({ message }) => (
    <SnackbarProvider>
        <Toasts message={message} />
    </SnackbarProvider>
)

ToastsContainer.propTypes = {
    message: PropTypes.object
}

export default ToastsContainer