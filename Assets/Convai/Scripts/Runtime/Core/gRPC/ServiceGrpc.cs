// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: grpc/service.proto
// </auto-generated>
// Original file comments:
// service.proto
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace Service {
  public static partial class ConvaiService
  {
    static readonly string __ServiceName = "service.ConvaiService";

    static readonly grpc::Marshaller<global::Service.HelloRequest> __Marshaller_service_HelloRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.HelloRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.HelloResponse> __Marshaller_service_HelloResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.HelloResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.STTRequest> __Marshaller_service_STTRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.STTRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.STTResponse> __Marshaller_service_STTResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.STTResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.GetResponseRequest> __Marshaller_service_GetResponseRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.GetResponseRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.GetResponseResponse> __Marshaller_service_GetResponseResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.GetResponseResponse.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.GetResponseRequestSingle> __Marshaller_service_GetResponseRequestSingle = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.GetResponseRequestSingle.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.FeedbackRequest> __Marshaller_service_FeedbackRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.FeedbackRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::Service.FeedbackResponse> __Marshaller_service_FeedbackResponse = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::Service.FeedbackResponse.Parser.ParseFrom);

    static readonly grpc::Method<global::Service.HelloRequest, global::Service.HelloResponse> __Method_Hello = new grpc::Method<global::Service.HelloRequest, global::Service.HelloResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Hello",
        __Marshaller_service_HelloRequest,
        __Marshaller_service_HelloResponse);

    static readonly grpc::Method<global::Service.HelloRequest, global::Service.HelloResponse> __Method_HelloStream = new grpc::Method<global::Service.HelloRequest, global::Service.HelloResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "HelloStream",
        __Marshaller_service_HelloRequest,
        __Marshaller_service_HelloResponse);

    static readonly grpc::Method<global::Service.STTRequest, global::Service.STTResponse> __Method_SpeechToText = new grpc::Method<global::Service.STTRequest, global::Service.STTResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "SpeechToText",
        __Marshaller_service_STTRequest,
        __Marshaller_service_STTResponse);

    static readonly grpc::Method<global::Service.GetResponseRequest, global::Service.GetResponseResponse> __Method_GetResponse = new grpc::Method<global::Service.GetResponseRequest, global::Service.GetResponseResponse>(
        grpc::MethodType.DuplexStreaming,
        __ServiceName,
        "GetResponse",
        __Marshaller_service_GetResponseRequest,
        __Marshaller_service_GetResponseResponse);

    static readonly grpc::Method<global::Service.GetResponseRequestSingle, global::Service.GetResponseResponse> __Method_GetResponseSingle = new grpc::Method<global::Service.GetResponseRequestSingle, global::Service.GetResponseResponse>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "GetResponseSingle",
        __Marshaller_service_GetResponseRequestSingle,
        __Marshaller_service_GetResponseResponse);

    static readonly grpc::Method<global::Service.FeedbackRequest, global::Service.FeedbackResponse> __Method_SubmitFeedback = new grpc::Method<global::Service.FeedbackRequest, global::Service.FeedbackResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SubmitFeedback",
        __Marshaller_service_FeedbackRequest,
        __Marshaller_service_FeedbackResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::Service.ServiceReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of ConvaiService</summary>
    [grpc::BindServiceMethod(typeof(ConvaiService), "BindService")]
    public abstract partial class ConvaiServiceBase
    {
      public virtual global::System.Threading.Tasks.Task<global::Service.HelloResponse> Hello(global::Service.HelloRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task HelloStream(grpc::IAsyncStreamReader<global::Service.HelloRequest> requestStream, grpc::IServerStreamWriter<global::Service.HelloResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task SpeechToText(grpc::IAsyncStreamReader<global::Service.STTRequest> requestStream, grpc::IServerStreamWriter<global::Service.STTResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task GetResponse(grpc::IAsyncStreamReader<global::Service.GetResponseRequest> requestStream, grpc::IServerStreamWriter<global::Service.GetResponseResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task GetResponseSingle(global::Service.GetResponseRequestSingle request, grpc::IServerStreamWriter<global::Service.GetResponseResponse> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::Service.FeedbackResponse> SubmitFeedback(global::Service.FeedbackRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ConvaiService</summary>
    public partial class ConvaiServiceClient : grpc::ClientBase<ConvaiServiceClient>
    {
      /// <summary>Creates a new client for ConvaiService</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ConvaiServiceClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ConvaiService that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ConvaiServiceClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ConvaiServiceClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ConvaiServiceClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::Service.HelloResponse Hello(global::Service.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Hello(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Service.HelloResponse Hello(global::Service.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Hello, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Service.HelloResponse> HelloAsync(global::Service.HelloRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HelloAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Service.HelloResponse> HelloAsync(global::Service.HelloRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Hello, null, options, request);
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Service.HelloRequest, global::Service.HelloResponse> HelloStream(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return HelloStream(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Service.HelloRequest, global::Service.HelloResponse> HelloStream(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_HelloStream, null, options);
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Service.STTRequest, global::Service.STTResponse> SpeechToText(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SpeechToText(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Service.STTRequest, global::Service.STTResponse> SpeechToText(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_SpeechToText, null, options);
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Service.GetResponseRequest, global::Service.GetResponseResponse> GetResponse(grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetResponse(new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncDuplexStreamingCall<global::Service.GetResponseRequest, global::Service.GetResponseResponse> GetResponse(grpc::CallOptions options)
      {
        return CallInvoker.AsyncDuplexStreamingCall(__Method_GetResponse, null, options);
      }
      public virtual grpc::AsyncServerStreamingCall<global::Service.GetResponseResponse> GetResponseSingle(global::Service.GetResponseRequestSingle request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return GetResponseSingle(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::Service.GetResponseResponse> GetResponseSingle(global::Service.GetResponseRequestSingle request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_GetResponseSingle, null, options, request);
      }
      public virtual global::Service.FeedbackResponse SubmitFeedback(global::Service.FeedbackRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SubmitFeedback(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::Service.FeedbackResponse SubmitFeedback(global::Service.FeedbackRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SubmitFeedback, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::Service.FeedbackResponse> SubmitFeedbackAsync(global::Service.FeedbackRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SubmitFeedbackAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::Service.FeedbackResponse> SubmitFeedbackAsync(global::Service.FeedbackRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SubmitFeedback, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ConvaiServiceClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ConvaiServiceClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ConvaiServiceBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Hello, serviceImpl.Hello)
          .AddMethod(__Method_HelloStream, serviceImpl.HelloStream)
          .AddMethod(__Method_SpeechToText, serviceImpl.SpeechToText)
          .AddMethod(__Method_GetResponse, serviceImpl.GetResponse)
          .AddMethod(__Method_GetResponseSingle, serviceImpl.GetResponseSingle)
          .AddMethod(__Method_SubmitFeedback, serviceImpl.SubmitFeedback).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ConvaiServiceBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Hello, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Service.HelloRequest, global::Service.HelloResponse>(serviceImpl.Hello));
      serviceBinder.AddMethod(__Method_HelloStream, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Service.HelloRequest, global::Service.HelloResponse>(serviceImpl.HelloStream));
      serviceBinder.AddMethod(__Method_SpeechToText, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Service.STTRequest, global::Service.STTResponse>(serviceImpl.SpeechToText));
      serviceBinder.AddMethod(__Method_GetResponse, serviceImpl == null ? null : new grpc::DuplexStreamingServerMethod<global::Service.GetResponseRequest, global::Service.GetResponseResponse>(serviceImpl.GetResponse));
      serviceBinder.AddMethod(__Method_GetResponseSingle, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::Service.GetResponseRequestSingle, global::Service.GetResponseResponse>(serviceImpl.GetResponseSingle));
      serviceBinder.AddMethod(__Method_SubmitFeedback, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::Service.FeedbackRequest, global::Service.FeedbackResponse>(serviceImpl.SubmitFeedback));
    }

  }
}
#endregion
