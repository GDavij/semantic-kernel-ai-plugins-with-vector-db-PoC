events {
    worker_connections 512;
}

http {
    server {
        listen *:8080;
        server_name ollama-test.internal;

        location / {
            resolver 127.0.0.11;
            set $upstream_host "presentation-ui";
            proxy_pass http://$upstream_host:3000;
            proxy_set_header Host $host;
            proxy_set_header X-Real-IP $proxy_protocol_addr;
            proxy_set_header X-Forwarded-For $proxy_protocol_addr;
        }

    }
}