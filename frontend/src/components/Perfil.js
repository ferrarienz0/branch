import React from 'react';
import { Image, Popup } from 'semantic-ui-react';

const Perfil = image => (
    <React.Fragment>
        <Popup
            content="conteudo"
            key="chave"
            header="nome"
            trigger={<Image avatar />}
        />
    </React.Fragment>
);

export default Perfil;
