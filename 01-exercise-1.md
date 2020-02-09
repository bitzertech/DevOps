# Exercise 1

## Install kubernetes cluster

Using minikube is the easiest.

Works with:

* Virtual box
* Docker for windows (don't let docker for windows install kubernetes)

## Check your configuration

View clusters and context's:

```shell
$ kubectl config view
```


To view nodes in the cluster, type:

```shell
$ kubectl get nodes
```

If you add the `-o wide` parameters to the above command, you will also see the public IP addresses of the nodes:

```shell
$ kubectl get nodes -o wide
```

**Note:** On local kubernetes clusters, you'll see master and worker nodes. On clusters provided by a service provider, only worker nodes will be see.

## Namespaces

Namespaces are the default way for kubernetes to separate resources.

### Create a namespace (optional)

Choose a name for your namespace.

```shell
$ kubectl create namespace my-namespace
namespace "my-namespace" created

$ kubectl get namespaces
```
You should now see your namespace created.

The default namespace for kubectl is 'default'.

```shell
$ kubectl get pods -n default
$ kubectl get pods
```

To overwrite the default namespace for your current `context`, run:

```shell
$ kubectl config set-context $(kubectl config current-context) --namespace=<my-namespace>
Context "<your current context>" modified.
```

You can verify that you've updated your current `context` by running:

```shell
kubectl config get-contexts
```

## Pods and Deployments

A **Pod** (*not container*) is the smallest building-block/worker-unit in Kubernetes, it has a specification of one or more containers and exists for the duration of the containers;

if all the containers stops or terminates, the Pod is stopped.

Usually a pod will be part of a **Deployment**; a more controlled or _robust_ way of running Pods.

A deployment can be configured to automatically delete stopped or exited Pods and start new ones, as well as run a number of identical Pods e.g. to provide high-availability.

### Create a pod

```shell
$ kubectl create deployment nginx --image=nginx:1.7.9
deployment.apps/nginx created
```

The `create`-command is great for imperative testing, and getting something up and running fast.

It creates a _deployment_ named `nginx`, which creates a _replicaset_, which starts a _pod_ using the docker image `nginx`.

Just so you know what we're talking about, you can check the objects you've created with `get <object>`, either one at a time, or all-together like below:

```shell
$ kubectl get deployment,replicaset,pod    # NB: no whitespace in the comma-separated list
```

> A ReplicaSet is something which deals with the number of copies of this pod.

#### Testing access to our Pod (optional)

Enable ingress via minikube.

Run the following command to tunnel traffic (requires admin rights):

```shell
$ minikube tunnel
```

To illustrate that we actually have a functioning web-server running in our pod, let's try exposing it to the internet and access it from a browser!

First use the following command to create a `service` for your `deployment`:

```shell
$ kubectl expose deployment nginx --port 80 --type NodePort
nginx exposed
```

Get the `service` called `nginx` and note down the NodePort:

```shell
$ kubectl get service nginx
NAME    TYPE       CLUSTER-IP      EXTERNAL-IP   PORT(S)        AGE
nginx   NodePort   10.96.223.218   10.96.223.218 80:32458/TCP   12s
```

Can open service by:

minikube service nginx


In this example, Kubernetes has chosen port `32458`.

Finally, look up the IP address of a node in the cluster with:

```shell
$ kubectl get nodes -o wide           # The -o wide flag makes the output more verbose, i.e. to include the IPs
NAME    STATUS   . . . INTERNAL-IP  EXTERNAL-IP     . . .
node1   Ready    . . . 10.123.0.8   <none>          . . .
```

Since your `service` is of type `NodePort` it will be exposed on _any_ of the nodes,
on the port from before, so choose one of the `EXTERNAL-IP`'s,
and point your web browser to the URL `<EXTERNAL-IP>:<PORT>`. Alternatively, if you
use e.g. curl from within the training infrastructure, you should use the <INTERNAL-IP>
address.
