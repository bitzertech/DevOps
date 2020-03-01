# Create deployment and service YAML

## Deployment
The deployment would be something like this (k8s/echoserver-deployment.yaml):

```bash
apiVersion: apps/v1
kind: Deployment
metadata:
  name: echoserver
spec:
  selector:
    matchLabels:
      app: frontend    # the label to match in the service yaml
  replicas: 1
  template:
    metadata:
      labels:
        app: frontend
    spec:
      containers:
      - name: echoserver
        image: k8s.gcr.io/echoserver:1.4
        ports:
        - containerPort: 8080
```

Apply the deployment with the command:

```bash
kubectl apply -f k8s/echoserver-deployment.yaml
```

Verify that the deployment is active:
```bash
kubectl get deployment -o wide

NAME         READY   UP-TO-DATE   AVAILABLE   AGE   CONTAINERS   IMAGES                      SELECTOR
echoserver   1/1     1            1           32m   echoserver   k8s.gcr.io/echoserver:1.4   app=frontend
```

## Service

Now create the service (k8s/echoserver-service.yaml):

```bash
apiVersion: v1
kind: Service
metadata:
  labels:
    app: echoserver
  name: echoserver
spec:
  type: NodePort
  ports:
  - port: 8080
    targetPort: 8080
  selector:
    app: frontend       # the deployment to map this service to
```

Apply the service:

```bash
kubectl apply -f k8s/echoserver-service.yaml
```

Verify that the service is active:
```bash
kubectl get service -o wide

NAME         TYPE       CLUSTER-IP      EXTERNAL-IP   PORT(S)
echoserver   NodePort   10.101.174.60   <none>        8080:30486/TCP
```
Pay attention to the ports - 8080 is the internal port, and 30486 is the static port on each node.

## Verify access

The cluster ip is the intenal ip address of the service. To get the external address of the node, type:
```bash
kubectl get nodes -o wide

NAME       STATUS   ROLES    AGE   VERSION   INTERNAL-IP     EXTERNAL-IP
minikube   Ready    master   26h   v1.17.3   172.17.253.26   <none>
```
You now get the node's internal ip and the static port. To verify the access, use the url: 172.17.253.26:30486 in a browser.

or type (with admin rights):
```bash
minikube service echoserver -n my-namespace
```
which opens the web browser with the correct ip and port.

## Cleanup

To cleanup deployment and service in one go, type:
```bash
kubectl delete -f k8s    # k8s is the folder containing the yaml files
```
